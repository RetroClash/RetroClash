using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Commands.Server
{
    public class LogicJoinAlliance : LogicCommand
    {
        public LogicJoinAlliance(Device device) : base(device)
        {
            Type = 1;
        }

        public long AllianceId { get; set; }
        public string AllianceName { get; set; }
        public int AllianceBadge { get; set; }
        public Enums.Role Role { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteLong(AllianceId);
            await Stream.WriteString(AllianceName);
            await Stream.WriteInt(AllianceBadge);
            Stream.WriteByte((byte) Role);
        }
    }
}