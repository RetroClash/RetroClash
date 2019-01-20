using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class HomeBattleReplayFailedMessage : PiranhaMessage
    {
        public HomeBattleReplayFailedMessage(Device device) : base(device)
        {
            Id = 24116;
        }

        public override async Task Encode()
        {
            await Stream.WriteInt(1);
        }
    }
}