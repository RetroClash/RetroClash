using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class NpcDataMessage : PiranhaMessage
    {
        public NpcDataMessage(Device device) : base(device)
        {
            Id = 24133;
            Device.State = Enums.State.Npc;
        }

        public int NpcId { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteInt(0);

            await Stream.WriteString(Resources.Levels.NpcLevels[NpcId - 17000000]);

            await Device.Player.LogicClientAvatar(Stream);

            await Stream.WriteInt(0);

            await Stream.WriteInt(NpcId);
        }
    }
}