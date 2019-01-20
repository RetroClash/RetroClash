using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class LoginFailedMessage : PiranhaMessage
    {
        public LoginFailedMessage(Device device) : base(device)
        {
            Id = 20103;
            Version = 2;
        }

        public int ErrorCode { get; set; }
        public int SecondsUntilMaintenanceEnds { get; set; }
        public string Reason { get; set; }
        public string ResourceFingerprintData { get; set; }

        // Codes:
        // 7 = Content Update
        // 8 = Update Available
        // 10 = Maintenance
        // 11 = Banned
        // 12 = Played too long

        public override async Task Encode()
        {
            await Stream.WriteVInt(ErrorCode); // ErrorCode
            await Stream.WriteString(ResourceFingerprintData); // Fingerprint
            await Stream.WriteString(null);
            await Stream.WriteString(Resources.Configuration.PatchUrl); // Content URL
            await Stream.WriteString(Resources.Configuration.UpdateUrl); // Update URL
            await Stream.WriteString(Reason);
            await Stream.WriteVInt(SecondsUntilMaintenanceEnds);
        }
    }
}