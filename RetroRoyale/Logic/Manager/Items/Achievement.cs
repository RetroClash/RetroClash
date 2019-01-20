using Newtonsoft.Json;

namespace RetroRoyale.Logic.Manager.Items
{
    public class Achievement
    {
        [JsonProperty("data")]
        public int Data { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}