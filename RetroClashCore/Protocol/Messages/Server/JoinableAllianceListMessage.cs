using System.IO;
using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class JoinableAllianceListMessage : PiranhaMessage
    {
        public JoinableAllianceListMessage(Device device) : base(device)
        {
            Id = 24304;
        }

        public override async Task Encode()
        {
            var clans = Resources.LeaderboardCache.JoinableClans;

            var count = 0;
            using (var buffer = new MemoryStream())
            {
                foreach (var clan in clans)
                {
                    if (clan == null) continue;
                    await buffer.WriteLong(clan.Id); // Id
                    await buffer.WriteString(clan.Name); // Name
                    await buffer.WriteInt(clan.Badge); // Badge
                    await buffer.WriteInt(clan.Type); // Type
                    await buffer.WriteInt(clan.Members.Count); // Member Count
                    await buffer.WriteInt(clan.Score); // Score
                    await buffer.WriteInt(clan.RequiredScore); // Required Score

                    if (count++ >= 39)
                        break;
                }


                await Stream.WriteInt(count);
                await Stream.WriteBuffer(buffer.ToArray());
            }
        }
    }
}