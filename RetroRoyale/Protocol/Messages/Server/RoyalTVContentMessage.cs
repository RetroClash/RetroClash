using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class RoyalTvContentMessage : PiranhaMessage
    {
        public RoyalTvContentMessage(Device device) : base(device)
        {
            Id = 24405;
        }

        public override async Task Encode()
        {
            await Stream.WriteVInt(0);
        }
    }
}