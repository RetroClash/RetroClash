using System.Threading.Tasks;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class OwnAvatarDataMessage : PiranhaMessage
    {
        public OwnAvatarDataMessage(Device device) : base(device)
        {
            Id = 24102;
        }

        public override async Task Encode()
        {
            await Device.Player.LogicClientAvatar(Stream);
        }
    }
}