using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class FriendListMessage : PiranhaMessage
    {
        public FriendListMessage(Device device) : base(device)
        {
            Id = 20105;
        }

        public override async Task Encode()
        {
            await Stream.WriteInt(1);
            await Stream.WriteInt(0);
        }
    }
}