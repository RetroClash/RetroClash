using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Server
{
    public class WaitingToGoHomeMessage : PiranhaMessage
    {
        public WaitingToGoHomeMessage(Device device) : base(device)
        {
            Id = 24112;
        }

        public int EstimatedTimeSeconds { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteInt(EstimatedTimeSeconds);
        }
    }
}