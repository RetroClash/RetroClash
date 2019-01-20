using System;
using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class LoginOkMessage : PiranhaMessage
    {
        public LoginOkMessage(Device device) : base(device)
        {
            Id = 20104;

            Device.LoginTime = DateTime.UtcNow;
        }

        public override async Task Encode()
        {
            await Stream.WriteLong(Device.Player.AccountId); // Account Id
            await Stream.WriteLong(Device.Player.AccountId); // Home Id
            await Stream.WriteString(Device.Player.PassToken); // Pass Token

            await Stream.WriteString(Device.Player.FacebookId); // Facebook Id
            await Stream.WriteString(null); // Gamecenter Id
           
            await Stream.WriteVInt(2); // Server Major
            await Stream.WriteVInt(1507); // Server Build
            await Stream.WriteVInt(0); // Content Version

            await Stream.WriteString("integration"); // Server Env

            await Stream.WriteVInt(0); // Session Count
            await Stream.WriteVInt(0); // PlayTimeSeconds
            await Stream.WriteVInt(0); // DaysSinceStartedPlaying

            await Stream.WriteString("171514200197498"); // Facebook App Id   

            await Stream.WriteString(null); // Server Time
            await Stream.WriteString(null); // AccountCreatedDate
        }
    }
}