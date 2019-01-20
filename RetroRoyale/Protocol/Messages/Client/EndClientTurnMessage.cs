using System;
using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroRoyale.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Client
{
    public class EndClientTurnMessage : PiranhaMessage
    {
        public EndClientTurnMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public int Count { get; set; }
        public int Tick { get; set; }

        public override void Decode()
        {
            Tick = Reader.ReadVInt(); // Tick
            Reader.ReadVInt(); // Checksum

            Count = Reader.ReadVInt();
        }

        public override async Task Process()
        {
            if (Math.Abs(Device.ServerTick - Tick) > 60)
            {
                Logger.Log("Warning client is out of sync.", Enums.LogType.Debug);
                await Resources.Gateway.Send(new OutOfSyncMessage(Device));
            }

            if (Count >= 0 && Count <= 512)
            {
                if (Count > 0)
                {
                    Save = true;

                    for (var index = 0; index < Count; index++)
                    {
                        var type = Reader.ReadVInt();

                        if (LogicCommandManager.Commands.ContainsKey(type))
                            try
                            {
                                if (Activator.CreateInstance(LogicCommandManager.Commands[type], Device, Reader) is
                                    LogicCommand
                                    command)
                                {
                                    command.SubTick = Reader.ReadVInt();
                                    Reader.ReadVInt();
                                    command.Type = type;

                                    command.Decode();

                                    await command.Process();                                   

                                    Logger.Log($"Command {type} with SubTick {command.SubTick} has been processed.", Enums.LogType.Debug);

                                    command.Dispose();
                                }
                            }
                            catch (Exception exception)
                            {
                                Logger.Log(exception, Enums.LogType.Error);
                            }
                        else
                            Logger.Log($"Command {type} is unhandled.", Enums.LogType.Warning);
                    }
                }
            }
            else
            {
                await Resources.Gateway.Send(new OutOfSyncMessage(Device));
            }
        }
    }
}