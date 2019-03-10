using System.Collections.Generic;
using Newtonsoft.Json;
using RetroClash.Logic.Manager;
using RetroClash.Logic.Replay.Items;

namespace RetroClash.Logic.Replay
{
    public class LogicReplay
    {
        [JsonProperty("cmd")] public List<ReplayCommand> Commands = new List<ReplayCommand>();

        [JsonProperty("level")]
        public LogicGameObjectManager Level { get; set; }

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }

        [JsonProperty("end_tick")]
        public int EndTick { get; set; }

        [JsonProperty("attacker")]
        public ReplayProfile Attacker { get; set; }

        [JsonProperty("defender")]
        public ReplayProfile Defender { get; set; }
    }
}