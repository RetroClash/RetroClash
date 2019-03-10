using System.Threading.Tasks;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Client
{
    public class VisitHomeMessage : PiranhaMessage
    {
        public VisitHomeMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public long UserId { get; set; }

        public override void Decode()
        {
            UserId = Reader.ReadInt64();
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new VisitedHomeDataMessage(Device)
            {
                AvatarId = UserId
            });
        }
    }
}