using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Commands.Server
{
    public class LogicChangeAvatarNameCommand : LogicCommand
    {
        public LogicChangeAvatarNameCommand(Device device) : base(device)
        {
            Type = 201;
        }

        public string Name { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteString(Name);
            await Stream.WriteVInt(0);
            Stream.WriteByte(0);
        }
    }
}
