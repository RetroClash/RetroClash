using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroClashCore.Logic.Battle;
using RetroClashCore.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicMatchmakingCommand : LogicCommand
    {
        public LogicMatchmakingCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            var enemy = await Resources.PlayerCache.Random();

            await Resources.Gateway.Send(new EnemyHomeDataMessage(Device)
            {
                Enemy = enemy
            });

            if (Device.Player.Shield.IsShieldActive)
                Device.Player.Shield.RemoveShield();

            if (enemy != null && Device.State == Enums.State.Battle)
            {
                if (Device.Player.Battle == null)
                    Device.Player.Battle = new PvbBattle(Device.Player);

                Device.Player.Battle.SetDefender(enemy);
            }
        }
    }
}