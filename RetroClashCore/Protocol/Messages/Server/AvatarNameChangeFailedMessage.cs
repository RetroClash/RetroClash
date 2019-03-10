using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Server
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