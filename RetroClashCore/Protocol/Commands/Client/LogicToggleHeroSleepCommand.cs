using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicToggleHeroSleepCommand : LogicCommand
    {
        public LogicToggleHeroSleepCommand(Device device, Reader reader) : base(device, reader)
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

                hero?.Toogle();
            });
        }
    }
}