using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;
using RetroRoyale.Protocol.Messages.Server;

namespace RetroRoyale.Protocol.Commands.Client
{
    public class LogicChallengeCommand : LogicCommand
    {
        public LogicChallengeCommand(Device device, Reader reader) : base(device, reader)
        {       
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new MatchmakeInfoMessage(Device)
            {
                EstimatedDuration = 3600
            });
        }
    }
}
