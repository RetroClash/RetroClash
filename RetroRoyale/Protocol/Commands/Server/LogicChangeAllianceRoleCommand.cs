using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Commands.Server
{
    public class LogicChangeAllianceRoleCommand : LogicCommand
    {
        public LogicChangeAllianceRoleCommand(Device device) : base(device)
        {
            Type = 210;
        }

        public long AccountId { get; set; }
        public int Role { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteLong(AccountId);
            await Stream.WriteVInt(Role);
        }
    }
}
