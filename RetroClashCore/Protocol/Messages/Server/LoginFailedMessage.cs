using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class LoginFailedMessage : PiranhaMessage
    {
        public LoginFailedMessage(Device device) : base(device)
        {
            Id = 20103;
            Version = 2;
        }

        public int ErrorCode { get; set; }
        public string Reason { get; set; }
        public string Fingerprint { get; set; }

        // Codes:
        // 7 = Content Update
        // 8 = Update Available
        // 10 = Maintenance
        // 11 = Banned
        // 12 = Played too long

        public override async Task Encode()
        {
            await Stream.WriteInt(ErrorCode);
            await Stream.WriteString(Fingerprint); // Fingerprint
            await Stream.WriteString(null);
            await Stream.WriteString(Resources.Configuration.PatchUrl); // Content URL
            await Stream.WriteString(Resources.Configuration.UpdateUrl); // Update URL
            await Stream.WriteString(Reason);
        }
    }
}