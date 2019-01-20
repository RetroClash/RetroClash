using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicEndCombatCommand : LogicCommand
    {
        public LogicEndCombatCommand(Device device, Reader reader) : base(device, reader)
        {
        }
    }
}