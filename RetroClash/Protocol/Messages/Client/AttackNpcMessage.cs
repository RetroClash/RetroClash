using System.Threading.Tasks;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Client
{
    public class AttackNpcMessage : PiranhaMessage
    {
        public AttackNpcMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public int LevelId { get; set; }

        public override void Decode()
        {
            LevelId = Reader.ReadInt32();
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new NpcDataMessage(Device)
            {
                NpcId = LevelId
            });
        }
    }
}