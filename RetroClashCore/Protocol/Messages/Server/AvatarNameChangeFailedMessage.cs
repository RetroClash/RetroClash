using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class AvatarNameChangeFailedMessage : PiranhaMessage
    {
        public AvatarNameChangeFailedMessage(Device device) : base(device)
        {
            Id = 20205;
        }

        public override async Task Encode()
        {
            await Stream.WriteInt(1);
        }
    }
}