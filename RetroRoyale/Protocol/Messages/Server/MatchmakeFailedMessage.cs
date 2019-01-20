using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class MatchmakeFailedMessage : PiranhaMessage
    {
        public MatchmakeFailedMessage(Device device) : base(device)
        {
            Id = 24108;
        }

        public override async Task Encode()
        {
            await Stream.WriteInt(0);
        }
    }
}