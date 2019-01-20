using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class VisitedHomeDataMessage : PiranhaMessage
    {
        public VisitedHomeDataMessage(Device device) : base(device)
        {
            Id = 24113;
        }

        public long HomeId { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteVInt(0);

            if (HomeId == Device.Player.AccountId)
            {
                await Device.Player.LogicClientHome(Stream);
                await Device.Player.LogicClientAvatar(Stream);
            }
            else
            {
                var player = await Resources.PlayerCache.GetPlayer(HomeId);

                await player.LogicClientHome(Stream);
                await player.LogicClientAvatar(Stream);
            }
        }
    }
}