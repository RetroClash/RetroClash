using System;
using System.Threading;
using System.Threading.Tasks;
using RetroClashCore.Network;
using RetroClashCore.Protocol;
using RetroGames.Crypto.RC4;
using RetroGames.Helpers;

namespace RetroClashCore.Logic
{
    public class Device : IDisposable
    {
        public DateTime LastChatMessage = DateTime.UtcNow;
        public DateTime LastKeepAlive = DateTime.UtcNow;
        public Rc4Core Rc4 = new Rc4Core(Resources.Configuration.EncryptionKey, "nonce");
        public Enums.State State = Enums.State.Login;

        public Device(UserToken userToken)
        {
            UserToken = userToken;
            SessionId = Guid.NewGuid();
        }

        public UserToken UserToken { get; set; }
        public Player Player { get; set; }
        public Guid SessionId { get; set; }

        public long TimeSinceLastKeepAlive => (long) DateTime.UtcNow.Subtract(LastKeepAlive).TotalSeconds;

        public void Dispose()
        {
            Rc4 = null;
            UserToken = null;
            Player = null;
            SessionId = Guid.Empty;
        }

        public async Task HandleMessage(byte[] buffer)
        {
            if (buffer.Length >= 7)
                using (var cancellation = new CancellationTokenSource())
                {
                    using (var reader = new Reader(buffer))
                    {
                        var identifier = reader.ReadUInt16();

                        var length = (ushort) reader.ReadUInt24();

                        if (buffer.Length - 7 < length) return;

                        if (identifier >= 10000 && identifier < 20000)
                        {
                            if (!LogicMagicMessageFactory.Messages.ContainsKey(identifier))
                            {
                                if (Configuration.Debug)
                                    Disconnect();

                                Logger.Log($"Message {identifier} is not known.", Enums.LogType.Warning);
                            }
                            else
                            {
                                cancellation.CancelAfter(2000);

                                if (Activator.CreateInstance(LogicMagicMessageFactory.Messages[identifier], this,
                                        reader) is
                                    PiranhaMessage
                                    message)
                                    try
                                    {
                                        message.Id = identifier;
                                        message.Length = length;
                                        message.Version = reader.ReadUInt16();

                                        await Task.Factory.StartNew(async () =>
                                        {
                                            message.Decrypt();
                                            message.Decode();

                                            await message.Process();

                                            //Logger.Log($"Message {identifier} has been handled.", Enums.LogType.Debug);

                                            if (State > Enums.State.Login && message.Save)
                                                await Player.Update();

                                            message.Dispose();
                                        }, cancellation.Token);
                                    }
                                    catch (OperationCanceledException)
                                    {
                                        Logger.Log(
                                            $"The operation for message {identifier} was aborted after 2 seconds.",
                                            Enums.LogType.Debug);
                                    }
                                    catch (Exception exception)
                                    {
                                        Logger.Log(exception, Enums.LogType.Error);
                                    }
                            }

                            if (buffer.Length - 7 - length >= 7)
                                await HandleMessage(reader.ReadBytes(buffer.Length - 7 - length));
                            else
                                UserToken.Reset();
                        }
                        else
                        {
                            Disconnect();
                        }
                    }
                }
        }

        public void Disconnect()
        {
            try
            {
                if (UserToken?.Socket == null) return;
                Resources.Gateway.DissolveSocket(UserToken.Socket);
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }
    }
}