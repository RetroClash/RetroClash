using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Commands.Server
{
    public class LogicDonateAllianceUnitCommand : LogicCommand
    {
        public LogicDonateAllianceUnitCommand(Device device) : base(device)
        {
            Type = 203;
        }

        public int CardId { get; set; }
        public long SenderId { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteVInt(CardId);
            await Stream.WriteLong(SenderId);
        }
    }
}
