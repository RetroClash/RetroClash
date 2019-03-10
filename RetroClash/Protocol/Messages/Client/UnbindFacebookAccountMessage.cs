using System.Threading.Tasks;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Client
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