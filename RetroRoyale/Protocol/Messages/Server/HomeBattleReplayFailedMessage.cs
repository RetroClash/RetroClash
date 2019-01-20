using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class HomeBattleReplayFailedMessage : PiranhaMessage
    {
        public HomeBattleReplayFailedMessage(Device device) : base(device)
        {
            Id = 24116;
        }

        public override async Task Encode()
        {
            await Stream.WriteInt(0); 
        }
    }
}