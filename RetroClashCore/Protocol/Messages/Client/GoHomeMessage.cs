using System.Threading.Tasks;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Client
{
    public class GoHomeMessage : PiranhaMessage
    {
        public GoHomeMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            if (Device.State == Enums.State.Battle)
            {
                await Device.Player.Battle.EndBattle();

                Device.Player.Battle.Dispose();

                Save = true;
            }

            if (Device.State != Enums.State.Home)
            {
                await Resources.Gateway.Send(new OwnHomeDataMessage(Device));

                await Resources.Gateway.Send(new AvatarStreamMessage(Device));
            }
            else
            {
                Device.Disconnect();
            }
        }
    }
}