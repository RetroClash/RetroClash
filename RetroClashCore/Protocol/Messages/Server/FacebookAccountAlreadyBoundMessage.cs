using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class FacebookAccountAlreadyBoundMessage : PiranhaMessage
    {
        public FacebookAccountAlreadyBoundMessage(Device device) : base(device)
        {
            Id = 24262;
        }

        public Player Player { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteString(Player.FacebookId);

            Stream.WriteByte(1);

            await Stream.WriteLong(Player.AccountId);
            await Stream.WriteString(Player.PassToken);

            await Player.LogicClientAvatar(Stream);
        }
    }
}