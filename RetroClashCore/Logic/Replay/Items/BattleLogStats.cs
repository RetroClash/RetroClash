using Newtonsoft.Json;

namespace RetroClashCore.Logic.Replay.Items
{
    public class BattleLogStats
    {
        [JsonProperty("townhallDestroyed")]
        public bool TownHallDestroyed { get; set; }

        [JsonProperty("battleEnded")]
        public bool BattleEnded { get; set; }

        [JsonProperty("allianceUsed")]
        public bool AllianceUsed { get; set; }

        [JsonProperty("destructionPercentage")]
        public int DestructionPercentage { get; set; }

        [JsonProperty("battleTime")]
        public int BattleTime { get; set; }

        [JsonProperty("attackerScore")]
        public int AttackerScore { get; set; }

        [JsonProperty("defenderScore")]
        public int DefenderScore { get; set; }

        [JsonProperty("originalScore")]
        public int OriginalScore { get; set; }

        [JsonProperty("allianceName")]
        public string AllianceName { get; set; }

        [JsonProperty("homeID")]
        public int[] HomeId { get; set; }
    }
}