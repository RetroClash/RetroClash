using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Commands.Server
{
    public class LogicLeaveAllianceCommand : LogicCommand
    {
        public LogicLeaveAllianceCommand(Device device) : base(device)
        {
            Type = 208;
        }

        public long AllianceId { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteLong(AllianceId);
            await Stream.WriteVInt(0);
            await Stream.WriteVInt(1); 
        }
    }
}
