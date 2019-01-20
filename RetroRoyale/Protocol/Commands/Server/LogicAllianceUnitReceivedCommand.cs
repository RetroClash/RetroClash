using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Commands.Server
{
    public class LogicAllianceSettingsChangedCommand : LogicCommand
    {
        public LogicAllianceSettingsChangedCommand(Device device) : base(device)
        {
            Type = 212;
        }

        public string SenderName { get; set; }
        public int CardClassId { get; set; }
        public int CardInstanceId { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteString(SenderName);
            await Stream.WriteVInt(CardClassId);
            await Stream.WriteVInt(CardInstanceId);
        }
    }
}
