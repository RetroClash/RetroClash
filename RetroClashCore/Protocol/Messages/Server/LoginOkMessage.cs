using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Server
{
    public class LoginOkMessage : PiranhaMessage
    {
        public LoginOkMessage(Device device) : base(device)
        {
            Id = 20104;
            Version = 1;
        }

        public override async Task Encode()
        {
            await Stream.WriteLong(Device.Player.AccountId); // Account Id
            await Stream.WriteLong(Device.Player.AccountId); // Home Id
            await Stream.WriteString(Device.Player.PassToken); // Pass Token

            await Stream.WriteString(Device.Player.FacebookId); // Facebook Id
            await Stream.WriteString(null); // Gamecenter Id

            await Stream.WriteInt(5); // Server Major
            await Stream.WriteInt(0); // Server Build
            await Stream.WriteInt(0); // Content Version

            await Stream.WriteString("integration"); // Server Env

            await Stream.WriteInt(0); // Session Count
            await Stream.WriteInt(0); // PlayTimeSeconds
            await Stream.WriteInt(0); // DaysSinceStartedPlaying

            await Stream.WriteString("171514200197498"); // Facebook App Id       
        }
    }
}