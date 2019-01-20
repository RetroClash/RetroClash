using System.IO;
using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class AvatarRankingListMessage : PiranhaMessage
    {
        public AvatarRankingListMessage(Device device) : base(device)
        {
            Id = 24403;
        }

        public override async Task Encode()
        {
            /*var count = 0;

            using (var buffer = new MemoryStream())
            {
                foreach (var player in Resources.LeaderboardCache.GlobalPlayers)
                {
                    if (player == null) continue;

                    await buffer.WriteVInt(player.HighId); // HighId
                    await buffer.WriteVInt(player.LowId); // LowId
                    await buffer.WriteString(player.Name); // Name
                    await buffer.WriteVInt(count + 1); // Order
                    await buffer.WriteVInt(player.Score); // Score
                    await buffer.WriteVInt(200); // PreviousOrder

                    await player.AvatarRankingEntry(buffer);

                    count++;
                }

                await Stream.WriteVInt(count);
                await Stream.WriteBuffer(buffer.ToArray());
            }*/
            await Stream.WriteVInt(0);

            await Stream.WriteInt(0);
            await Stream.WriteInt(Utils.GetSecondsUntilNextMonth);
        }
    }
}