using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicLeagueNotificationsSeenCommand : LogicCommand
    {
        public LogicLeagueNotificationsSeenCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            await Task.Run(() => { Device.Player.LogicGameObjectManager.LastLeagueRank = Reader.ReadInt32(); });
        }
    }
}