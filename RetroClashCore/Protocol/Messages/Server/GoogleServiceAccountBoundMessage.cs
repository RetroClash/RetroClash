using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class GoogleServiceAccountBoundMessage : PiranhaMessage
    {
        public GoogleServiceAccountBoundMessage(Device device) : base(device)
        {
            Id = 24261;
        }

        public override async Task Encode()
        {
            await Stream.WriteInt(1);
        }
    }
}