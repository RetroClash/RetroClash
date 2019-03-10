using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroGames.Helpers;

namespace RetroClash.Logic.StreamEntry.Avatar
{
    public class AllianceKickOutStreamEntry : AvatarStreamEntry
    {
        public AllianceKickOutStreamEntry()
        {
            StreamEntryType = 5;
        }

        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("alliance_id")]
        public long AllianceId { get; set; }

        [JsonProperty("alliance_name")]
        public string AllianceName { get; set; }

        [JsonProperty("alliance_badge")]
        public int AllianceBadge { get; set; }

        [JsonProperty("sender_home_id")]
        public long SenderHomeId { get; set; }

        public override async Task Encode(MemoryStream stream)
        {
            await base.Encode(stream);

            await stream.WriteString(Message);

            await stream.WriteLong(AllianceId); // AllianceId
            await stream.WriteString(AllianceName); // AllianceName
            await stream.WriteInt(AllianceBadge); // AllianceBadge

            if (SenderHomeId > 0)
            {
                stream.WriteByte(1);
                await stream.WriteLong(SenderHomeId); // SenderHomeId
            }
            else
            {
                stream.WriteByte(0);
            }
        }
    }
}