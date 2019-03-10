using Newtonsoft.Json;

namespace RetroClash.Logic.Replay.Items
{
    public class ReplayCommandBase
    {
        [JsonProperty("t")]
        public int Tick { get; set; }
    }
}