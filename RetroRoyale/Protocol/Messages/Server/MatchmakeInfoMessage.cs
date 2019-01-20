using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class MatchmakeInfoMessage : PiranhaMessage
    {
        public MatchmakeInfoMessage(Device device) : base(device)
        {
            Id = 24107;
        }

        public int EstimatedDuration { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteInt(EstimatedDuration); // EstimatedDuration
        }
    }
}