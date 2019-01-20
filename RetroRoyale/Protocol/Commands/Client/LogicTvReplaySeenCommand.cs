using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Commands.Client
{
    public class LogicTvReplaySeenCommand : LogicCommand
    {
        public LogicTvReplaySeenCommand(Device device, Reader reader) : base(device, reader)
        {       
        }

        public override void Decode()
        {
            Reader.ReadVInt();
            Reader.ReadVInt();
        }
    }
}
