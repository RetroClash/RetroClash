using System.IO;
using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class AvatarLocalRankingListMessage : PiranhaMessage
    {
        public AvatarLocalRankingListMessage(Device device) : base(device)
        {
            Id = 24404;
        }

        public override async Task Encode()
        {
            if (Resources.LeaderboardCache.LocalPlayers.ContainsKey(Device.Player.Language))
            {
                var count = 0;

                using (var buffer = new MemoryStream())
                {
                    foreach (var player in Resources.LeaderboardCache.LocalPlayers[Device.Player.Language])
                    {
                        if (player == null) continue;
                        await buffer.WriteLong(player.AccountId);
                        await buffer.WriteString(player.Name);

                        await buffer.WriteInt(count + 1);
                        await buffer.WriteInt(player.Score);
                        await buffer.WriteInt(200);

                        await player.AvatarRankingEntry(buffer);

                        count++;
                    }

                    await Stream.WriteInt(count);
                    await Stream.WriteBuffer(buffer.ToArray());
                }
            }
            else
            {
                await Stream.WriteInt(0);
            }
        }
    }
}