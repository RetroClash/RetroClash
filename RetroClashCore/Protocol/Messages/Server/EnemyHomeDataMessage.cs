using System;
using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Server
{
    public class EnemyHomeDataMessage : PiranhaMessage
    {
        public EnemyHomeDataMessage(Device device) : base(device)
        {
            Id = 24107;
        }

        public Player Enemy { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteInt(10);

            if (Enemy != null)
            {
                await Enemy.LogicClientHome(Stream);
                await Enemy.LogicClientAvatar(Stream);

                Device.State = Enums.State.Battle;
            }
            else
            {
                await Stream.WriteInt(0);

                await Stream.WriteLong(Device.Player.AccountId);

                await Stream.WriteString(
                    Resources.Levels.Prebases[new Random().Next(Resources.Levels.Prebases.Count - 1)]);

                await Stream.WriteInt(0); // Defense Rating
                await Stream.WriteInt(0); // Defense Factor
                await Stream.WriteInt(0);

                await Device.Player.LogicClientAvatar(Stream);

                Device.State = Enums.State.Npc;
            }

            await Device.Player.LogicClientAvatar(Stream);

            await Stream.WriteInt(3);
            Stream.WriteByte(0);
        }
    }
}