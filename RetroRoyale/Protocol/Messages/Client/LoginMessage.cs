using System;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using RetroRoyale.Database;
using RetroRoyale.Logic;
using RetroRoyale.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Client
{
    public class LoginMessage : PiranhaMessage
    {
        public LoginMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public long AccountId { get; set; }
        public string PassToken { get; set; }
        public string PreferredDeviceLanguage { get; set; }
        public string ResourceSha { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }

        public override void Decode()
        {
            AccountId = Reader.ReadInt64(); // Account Id
            PassToken = Reader.ReadString(); // PassToken

            Major = Reader.ReadVInt(); // Client Major Version
            Minor = Reader.ReadVInt(); // Client Minor Version
            Build = Reader.ReadVInt(); // Client Build 

            ResourceSha = Reader.ReadString(); // ResourceSha

            Reader.ReadString();
            Reader.ReadString();
            Reader.ReadString();
            Reader.ReadString();
            Reader.ReadString();
            Reader.ReadString();

            Reader.ReadByte();

            Reader.ReadString();
            Reader.ReadString();
            PreferredDeviceLanguage = Reader.ReadString(); // PreferredDeviceLanguage

            Reader.ReadString();

            Reader.ReadString();

            Reader.ReadByte();
            Reader.ReadString();

            Reader.ReadVInt();

            Reader.ReadString();
            Reader.ReadString();
        }

        public override async Task Process()
        {
            if (Device.State == Enums.State.Login || PreferredDeviceLanguage.Length >= 2)
                if (Configuration.Maintenance)
                {
                    await Resources.Gateway.Send(new LoginFailedMessage(Device) {ErrorCode = 10, SecondsUntilMaintenanceEnds = (int)(Program.MaintenanceEndTime - DateTime.UtcNow).TotalSeconds});
                }
                else
                {
                    if (ResourceSha == Resources.Fingerprint.Sha)
                        if (Resources.PlayerCache.Count < Configuration.MaxClients)
                        {
                            if (AccountId == 0)
                            {
                                Device.Player = await PlayerDb.Create();

                                if (Device.Player != null)
                                {
                                    Device.Player.Language = PreferredDeviceLanguage.ToUpper();
                                    Device.Player.IpAddress =
                                        ((IPEndPoint) Device.UserToken.EventArgs.AcceptSocket.RemoteEndPoint).Address
                                        .ToString();
                                    Device.Player.Device = Device;

                                    await Resources.Gateway.Send(new LoginOkMessage(Device));                                 

                                    await Resources.PlayerCache.AddPlayer(AccountId, Device.Player);

                                    await Resources.Gateway.Send(new OwnHomeDataMessage(Device));
                                }
                                else
                                {
                                    await Resources.Gateway.Send(new LoginFailedMessage(Device)
                                    {
                                        ErrorCode = 10,
                                        Reason =
                                            "An error occured during the creation of your account. Please contact the administrators of this server."
                                    });
                                }
                            }
                            else
                            {
                                if (AccountId > 0 && Resources.PlayerCache.ContainsKey(AccountId))
                                {
                                    await Resources.Gateway.Send(
                                        new DisconnectedMessage(Resources.PlayerCache[AccountId].Device));
                                    await Resources.PlayerCache.RemovePlayer(AccountId,
                                        Resources.PlayerCache[AccountId].Device.SessionId);
                                }

                                Device.Player = await Resources.PlayerCache.GetPlayer(AccountId);

                                if (Device.Player != null && Device.Player.PassToken == PassToken)
                                {
                                    Device.Player.Language = PreferredDeviceLanguage.ToUpper();

                                    Device.Player.Device = Device;

                                    if (await Resources.PlayerCache.AddPlayer(AccountId, Device.Player))
                                    {
                                        await Resources.Gateway.Send(new LoginOkMessage(Device));

                                        await Resources.Gateway.Send(new OwnHomeDataMessage(Device));
                                    }
                                    else
                                        await Resources.Gateway.Send(new LoginFailedMessage(Device)
                                        {
                                            ErrorCode = 10,
                                            Reason =
                                                "The server couldn't cache player."
                                        });
                                }
                                else
                                {
                                    await Resources.Gateway.Send(new LoginFailedMessage(Device)
                                    {
                                        ErrorCode = 10,
                                        Reason =
                                            "We couldn't find your account in our systems or your token is invalid."
                                    });
                                }
                            }
                        }
                        else
                        {
                            await Resources.Gateway.Send(new LoginFailedMessage(Device)
                            {
                                ErrorCode = 10,
                                Reason = "The server is currently full."
                            });
                        }
                    else
                        await Resources.Gateway.Send(new LoginFailedMessage(Device)
                        {
                            ErrorCode = 7,
                            ResourceFingerprintData = Resources.Fingerprint.Json
                        });
                }
            else
                Device.Disconnect();
        }
    }
}