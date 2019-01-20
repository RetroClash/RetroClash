using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class GlobalChatLineMessage : PiranhaMessage
    {
        public GlobalChatLineMessage(Device device) : base(device)
        {
            Id = 24715;
        }

        public string Message { get; set; }
        public string Name { get; set; }

        public int ExpLevel { get; set; }
        public int League { get; set; }

        public long AccountId { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteString(Message); // Message
            await Stream.WriteString(Name); // Name

            await Stream.WriteInt(ExpLevel); // ExpLevel
            await Stream.WriteInt(League); // League

            await Stream.WriteLong(AccountId); // AccountId
            await Stream.WriteLong(AccountId); // HomeId

            Stream.WriteByte(0); // IsInClan
        }
    }
}