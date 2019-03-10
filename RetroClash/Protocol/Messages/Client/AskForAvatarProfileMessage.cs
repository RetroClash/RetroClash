using System.Threading.Tasks;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;
using RetroGames.Helpers;
using RetroGames.Logic;

namespace RetroClash.Protocol.Messages.Client
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