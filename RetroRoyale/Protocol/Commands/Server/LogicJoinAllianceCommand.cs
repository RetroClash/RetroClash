using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Commands.Server
{
    public class LogicJoinAllianceCommand : LogicCommand
    {
        public LogicJoinAllianceCommand(Device device) : base(device)
        {
            Type = 209;
        }

        public long AllianceId { get; set; }
        public string AllianceName { get; set; }
        public int AllianceBadgeData { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteLong(AllianceId);
            await Stream.WriteString(AllianceName);

            await Stream.WriteVInt(16); // ClassId
            await Stream.WriteVInt(AllianceBadgeData); // InstanceId

            await Stream.WriteVInt(0);
        }
    }
}
