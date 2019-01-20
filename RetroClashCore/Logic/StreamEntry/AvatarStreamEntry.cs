using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroGames.Helpers;

namespace RetroClashCore.Logic.StreamEntry
{
    public class AvatarStreamEntry
    {
        [JsonProperty("type")]
        public int StreamEntryType { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("sender_id")]
        public long SenderAvatarId { get; set; }

        [JsonProperty("sender_name")]
        public string SenderName { get; set; }

        [JsonProperty("sender_lvl")]
        public int SenderLevel { get; set; }

        [JsonProperty("sender_league")]
        public int SenderLeagueType { get; set; }

        [JsonProperty("creation")]
        public DateTime CreationDateTime { get; set; }

        [JsonIgnore]
        public int AgeSeconds => (int) (DateTime.UtcNow - CreationDateTime).TotalSeconds;

        public virtual async Task Encode(MemoryStream stream)
        {
            await stream.WriteInt(StreamEntryType); // StreamEntryType
            await stream.WriteLong(Id); // StreamEntryId
            await stream.WriteLong(SenderAvatarId); // SenderAvatarId
            await stream.WriteString(SenderName); // SenderName
            await stream.WriteInt(SenderLevel); // SenderLevel
            await stream.WriteInt(SenderLeagueType); // SenderLeagueType
            await stream.WriteInt(AgeSeconds); // AgeSeconds
            stream.WriteBool(false); // IsNew
        }

        public void SetSender(Player player)
        {
            SenderName = player.Name;
            SenderAvatarId = player.AccountId;
            SenderLevel = player.ExpLevel;
            SenderLeagueType = LogicUtils.GetLeagueByScore(player.Score);
        }
    }
}