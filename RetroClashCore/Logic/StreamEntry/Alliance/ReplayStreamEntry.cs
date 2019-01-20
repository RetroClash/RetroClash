using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroGames.Helpers;

namespace RetroClashCore.Logic.StreamEntry.Alliance
{
    public class ReplayStreamEntry : AllianceStreamEntry
    {
        public ReplayStreamEntry()
        {
            StreamEntryType = 5;
        }

        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("replay_shard_id")]
        public int ReplayShardId { get; set; }

        [JsonProperty("replay_id")]
        public long ReplayId { get; set; }

        [JsonProperty("opponent_name")]
        public string OpponentName { get; set; }

        [JsonProperty("battle_json")]
        public string BattleJson { get; set; }

        [JsonProperty("m_v")]
        public int MajorVersion { get; set; }

        [JsonProperty("b_v")]
        public int BuildVersion { get; set; }

        [JsonProperty("c_v")]
        public int ContentVersion { get; set; }

        [JsonProperty("is_attack")]
        public bool IsAttack { get; set; }

        public override async Task Encode(MemoryStream stream)
        {
            await base.Encode(stream);

            await stream.WriteInt(ReplayShardId); // ReplayShardId
            await stream.WriteLong(ReplayId); // ReplayId
            stream.WriteBool(IsAttack);
            await stream.WriteString(Message); // Message
            await stream.WriteString(OpponentName); // OpponentName

            await stream.WriteInt(MajorVersion); // Major
            await stream.WriteInt(BuildVersion); // Build
            await stream.WriteInt(ContentVersion); // Content
        }
    }
}