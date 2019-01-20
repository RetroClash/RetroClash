using System.Collections.Generic;
using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroClashCore.Logic.StreamEntry;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
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