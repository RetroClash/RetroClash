using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class BattleReportStreamMessage : PiranhaMessage
    {
        public BattleReportStreamMessage(Device device) : base(device)
        {
            Id = 24413;
        }

        public override async Task Encode()
        {
            await Stream.WriteLong(0);
            await Stream.WriteVInt(0);
        }
    }
}