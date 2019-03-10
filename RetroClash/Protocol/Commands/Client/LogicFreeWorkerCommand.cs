using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicFreeWorkerCommand : LogicCommand
    {
        public LogicFreeWorkerCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public override void Decode()
        {
            Reader.ReadInt32();
            Reader.ReadByte();
        }
    }
}