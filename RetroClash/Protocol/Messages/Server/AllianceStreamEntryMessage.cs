using System.Threading.Tasks;
using RetroClash.Logic;
using RetroClash.Logic.StreamEntry;

namespace RetroClash.Protocol.Messages.Server
{
    public class AllianceStreamEntryMessage : PiranhaMessage
    {
        public AllianceStreamEntryMessage(Device device) : base(device)
        {
            Id = 24312;
        }

        public AllianceStreamEntry AllianceStreamEntry { get; set; }

        public override async Task Encode()
        {
            await AllianceStreamEntry.Encode(Stream);
        }
    }
}