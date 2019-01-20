using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class InboxCountMessage : PiranhaMessage
    {
        public InboxCountMessage(Device device) : base(device)
        {
            Id = 24447;
        }

        public int InboxNewMessageCount { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteInt(InboxNewMessageCount); // InboxNewMessageCount
        }
    }
}