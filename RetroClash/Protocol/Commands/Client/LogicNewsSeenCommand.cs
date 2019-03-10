using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicNewsSeenCommand : LogicCommand
    {
        public LogicNewsSeenCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            await Task.Run(() => { Device.Player.LogicGameObjectManager.LastNewsSeen = Reader.ReadInt32(); });
        }
    }
}