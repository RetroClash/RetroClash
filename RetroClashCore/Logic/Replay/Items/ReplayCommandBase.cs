using Newtonsoft.Json;

namespace RetroClashCore.Logic.Replay.Items
{
    public class ReplayCommandBase
    {
        [JsonProperty("t")]
        public int Tick { get; set; }
    }
}