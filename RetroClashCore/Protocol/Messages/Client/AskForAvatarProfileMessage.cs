using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroClashCore.Protocol.Messages.Server;
using RetroGames.Helpers;
using RetroGames.Logic;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class AskForAvatarProfileMessage : PiranhaMessage
    {
        public AskForAvatarProfileMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public LogicLong AvatarId { get; set; }

        public override void Decode()
        {
            AvatarId = Reader.ReadLogicLong();
            Reader.ReadInt64();
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new AvatarProfileMessage(Device)
            {
                UserId = AvatarId
            });
        }
    }
}