using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicUpgradeHeroCommand : LogicCommand
    {
        public LogicUpgradeHeroCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public int HeroId { get; set; }

        public override void Decode()
        {
            HeroId = Reader.ReadInt32();

            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            await Task.Run(() =>
            {
                var hero = Device.Player.HeroManager.Get(HeroId);

                if (hero == null) return;
                hero.Health = 0;
                hero.State = 3;
                hero.Upgrade();
            });
        }
    }
}