using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class InboxListMessage : PiranhaMessage
    {
        public InboxListMessage(Device device) : base(device)
        {
            Id = 24445;
        }

        public override async Task Encode()
        {
            await Stream.WriteInt(0);
        }
    }
}