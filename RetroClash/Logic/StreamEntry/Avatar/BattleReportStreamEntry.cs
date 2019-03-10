using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroGames.Helpers;

namespace RetroClash.Logic.StreamEntry.Avatar
{
    public class BattleReportStreamEntry : AvatarStreamEntry
    {
        public BattleReportStreamEntry()
        {
            StreamEntryType = 7;
        }

        [JsonProperty("sh_id")]
        public int ShardId { get; set; }

        [JsonProperty("rp_id")]
        public long ReplayId { get; set; }

        [JsonProperty("is_ru")]
        public bool IsRevengeUsed { get; set; }

        [JsonProperty("mj")]
        public int MajorVersion { get; set; }

        [JsonProperty("bd")]
        public int Build { get; set; }

        [JsonProperty("co")]
        public int ContentVersion { get; set; }

        [JsonProperty("bl")]
        public string BattleLogJson { get; set; }

        public override async Task Encode(MemoryStream stream)
        {
            await base.Encode(stream);

            await stream.WriteString(BattleLogJson); // BattleLogJson
            stream.WriteBool(IsRevengeUsed); // IsRevengeUsed

            await stream.WriteInt(MajorVersion); // MajorVersion
            await stream.WriteInt(Build); // Build
            await stream.WriteInt(ContentVersion); // ContentVersion

            stream.WriteBool(true);

            await stream.WriteLong(ReplayId);
            await stream.WriteInt(ShardId);
        }
    }
}