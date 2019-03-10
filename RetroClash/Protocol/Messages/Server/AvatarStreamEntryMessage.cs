using System.Threading.Tasks;
using RetroClash.Logic;
using RetroClash.Logic.StreamEntry;

namespace RetroClash.Protocol.Messages.Server
{
    public class AvatarStreamEntryMessage : PiranhaMessage
    {
        public AvatarStreamEntryMessage(Device device) : base(device)
        {
            Id = 24412;
        }

        public AvatarStreamEntry Entry { get; set; }

        public override async Task Encode()
        {
            await Entry.Encode(Stream);
        }
    }
}