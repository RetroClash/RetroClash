using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Client
{
    public class ClientCapabilitiesMessage : PiranhaMessage
    {
        public ClientCapabilitiesMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public int Ping { get; set; }

        public string ConnectionInterface { get; set; }

        public override void Decode()
        {
            Ping = Reader.ReadVInt();
            ConnectionInterface = Reader.ReadString();
        }
    }
}