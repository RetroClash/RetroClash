using System.Collections.Generic;
using System.Threading.Tasks;
using RetroClash.Logic;
using RetroClash.Logic.StreamEntry;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Server
{
    public class AllianceStreamMessage : PiranhaMessage
    {
        public AllianceStreamMessage(Device device) : base(device)
        {
            Id = 24311;
        }

        public List<AllianceStreamEntry> AllianceStream { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteInt(AllianceStream.Count);

            foreach (var entry in AllianceStream)
                await entry.Encode(Stream);
        }
    }
}