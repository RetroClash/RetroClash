using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;
using RetroRoyale.Protocol.Messages.Server;

namespace RetroRoyale.Protocol.Messages.Client
{
    public class AskForAvatarRankingListMessage : PiranhaMessage
    {
        public AskForAvatarRankingListMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new AvatarRankingListMessage(Device));
        }
    }
}