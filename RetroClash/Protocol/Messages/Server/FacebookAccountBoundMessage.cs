using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Server
{
    public class FacebookAccountBoundMessage : PiranhaMessage
    {
        public FacebookAccountBoundMessage(Device device) : base(device)
        {
            Id = 24201;
        }

        public override async Task Encode()
        {
            await Stream.WriteInt(1);
        }
    }
}