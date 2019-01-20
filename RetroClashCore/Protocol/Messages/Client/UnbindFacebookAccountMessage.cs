using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroClashCore.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class UnbindFacebookAccountMessage : PiranhaMessage
    {
        public UnbindFacebookAccountMessage(Device device, Reader reader) : base(device, reader)
        {
            Save = true;
        }

        public override async Task Process()
        {
            Device.Player.FacebookId = null;

            await Resources.Gateway.Send(new FacebookAccountUnboundMessage(Device));
        }
    }
}