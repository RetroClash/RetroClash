using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Commands.Server
{
    public class LogicDiamondsAddedCommand : LogicCommand
    {
        public LogicDiamondsAddedCommand(Device device) : base(device)
        {
            Type = 202;
        }

        public int DiamondCount { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteVInt(DiamondCount);
            await Stream.WriteString("GPA.0000-0000-0000-00000");
            Stream.WriteByte(1);
        }
    }
}
