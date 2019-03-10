using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicEndCombatCommand : LogicCommand
    {
        public LogicEndCombatCommand(Device device, Reader reader) : base(device, reader)
        {
        }
    }
}