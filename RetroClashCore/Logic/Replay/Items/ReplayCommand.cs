using Newtonsoft.Json;

namespace RetroClashCore.Logic.Replay.Items
{
    public class ReplayCommand
    {
        [JsonProperty("ct")]
        public int CommandType { get; set; }

        [JsonProperty("c")]
        public ReplayCommandInfo ReplayCommandInfo { get; set; }
    }
}