using System.IO;
using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class AllianceRankingListMessage : PiranhaMessage
    {
        public AllianceRankingListMessage(Device device) : base(device)
        {
            Id = 24401;
        }

        public override async Task Encode()
        {
            var count = 0;

            using (var buffer = new MemoryStream())
            {
                foreach (var alliance in Resources.LeaderboardCache.GlobalAlliances)
                {
                    if (alliance == null) continue;
                    await buffer.WriteLong(alliance.Id);
                    await buffer.WriteString(alliance.Name);
                    await buffer.WriteInt(count + 1);
                    await buffer.WriteInt(alliance.Score);
                    await buffer.WriteInt(200);

                    await alliance.AllianceRankingEntry(buffer);

                    if (count++ >= 199)
                        break;
                }

                await Stream.WriteInt(count);
                await Stream.WriteBuffer(buffer.ToArray());

                await Stream.WriteInt(Utils
                    .GetSecondsUntilNextMonth); // Tournament Seconds left - 7 Days -> 604800

                await Stream.WriteInt(3); // Reward Count
                await Stream.WriteInt(100000); // #1 Reward
                await Stream.WriteInt(10000); // #2 Reward
                await Stream.WriteInt(1000); // #3 Reward
            }
        }
    }
}