using System;
using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroClashCore.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class EndClientTurnMessage : PiranhaMessage
    {
        public EndClientTurnMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public int Count { get; set; }
        public int SubTick { get; set; }

        public override void Decode()
        {
            SubTick = Reader.ReadInt32(); // Tick
            Reader.ReadInt32(); // Checksum

            Count = Reader.ReadInt32();
        }

        public override async Task Process()
        {
            if (Count >= 0 && Count <= 512)
            {
                Device.LastKeepAlive = DateTime.UtcNow;

                if (Count > 0)
                {
                    Save = true;

                    if (Device.State == Enums.State.Battle)
                        Device.Player.Battle.Replay.EndTick = SubTick;

                    for (var index = 0; index < Count; index++)
                    {
                        var type = Reader.ReadInt32();

                        if (LogicCommandManager.Commands.ContainsKey(type))
                            try
                            {
                                if (Activator.CreateInstance(LogicCommandManager.Commands[type], Device, Reader) is
                                    LogicCommand
                                    command)
                                {
                                    command.SubTick = SubTick;
                                    command.Type = type;

                                    command.Decode();

                                    await command.Process();

                                    command.Dispose();

                                    //Logger.Log($"Command {type} with SubTick {SubTick} has been processed.", Enums.LogType.Debug);
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