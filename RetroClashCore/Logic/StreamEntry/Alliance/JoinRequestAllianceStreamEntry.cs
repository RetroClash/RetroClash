using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroGames.Helpers;

namespace RetroClashCore.Logic.StreamEntry.Alliance
{
    public class JoinRequestAllianceStreamEntry : AllianceStreamEntry
    {
        public JoinRequestAllianceStreamEntry()
        {
            StreamEntryType = 3;
        }

        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("responder_name")]
        public string ResponderName { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        public override async Task Encode(MemoryStream stream)
        {
            await base.Encode(stream);

            await stream.WriteString(Message); // Message
            await stream.WriteString(ResponderName); // ResponderName
            await stream.WriteInt(State); // State
        }
    }
}