using System.Threading.Tasks;
using RetroGames.Helpers;
using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class UdpConnectionInfoMessage : PiranhaMessage
    {
        public UdpConnectionInfoMessage(Device device) : base(device)
        {
            Id = 24112;
        }

        public int ServerPort { get; set; }

        public string ServerHost { get; set; }

        public byte[] Nonce { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteVInt(ServerPort); // ServerPort
            await Stream.WriteString(ServerHost); // ServerHost
            await Stream.WriteBuffer(Nonce); // Nonce
        }
    }
}