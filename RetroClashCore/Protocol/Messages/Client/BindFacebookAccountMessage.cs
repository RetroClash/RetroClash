using System.Threading.Tasks;
using RetroClashCore.Database;
using RetroClashCore.Logic;
using RetroClashCore.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class BindFacebookAccountMessage : PiranhaMessage
    {
        public BindFacebookAccountMessage(Device device, Reader reader) : base(device, reader)
        {
            Save = true;
        }

        public string FacebookId { get; set; }

        public override void Decode()
        {
            Reader.ReadByte(); // Force
            FacebookId = Reader.ReadString(); // Facebook Id
            Reader.ReadString(); // AuthenticationToken
        }

        public override async Task Process()
        {
            var player = await PlayerDb.Get(FacebookId);

            if (player == null)
            {
                Device.Player.FacebookId = FacebookId;
                await Resources.Gateway.Send(new FacebookAccountBoundMessage(Device));
            }
            else
            {
                if (Device.Player.AccountId != player.AccountId)
                {
                    await Resources.Gateway.Send(new FacebookAccountAlreadyBoundMessage(Device) {Player = player});
                }
                else
                {
                    Device.Player.FacebookId = FacebookId;
                    await Resources.Gateway.Send(new FacebookAccountBoundMessage(Device));
                }
            }
        }
    }
}