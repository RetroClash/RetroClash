using System.Threading.Tasks;
using RetroClashCore.Database;
using RetroClashCore.Logic;
using RetroClashCore.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class HomeBattleReplayMessage : PiranhaMessage
    {
        public HomeBattleReplayMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public long ReplayId { get; set; }

        public override void Decode()
        {
            ReplayId = Reader.ReadInt64();
            Reader.ReadInt32(); // ShardId
        }

        public override async Task Process()
        {
            var replay = await ReplayDb.Get(ReplayId);

            if (replay != null)
                await Resources.Gateway.Send(new HomeBattleReplayDataMessage(Device)
                {
                    Replay = replay
                });
            else
                await Resources.Gateway.Send(new HomeBattleReplayFailedMessage(Device));
        }
    }
}