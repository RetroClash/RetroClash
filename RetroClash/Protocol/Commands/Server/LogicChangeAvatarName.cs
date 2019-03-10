using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Commands.Server
{
    public class LogicChangeAvatarName : LogicCommand
    {
        public LogicChangeAvatarName(Device device) : base(device)
        {
            Type = 3;
        }

        public string AvatarName { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteString(AvatarName);
        }
    }
}