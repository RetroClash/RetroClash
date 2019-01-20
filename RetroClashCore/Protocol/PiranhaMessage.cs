using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol
{
    public class PiranhaMessage : IDisposable
    {
        public PiranhaMessage(Device device)
        {
            Device = device;
            Stream = new MemoryStream();
        }

        public PiranhaMessage(Device device, Reader reader)
        {
            Device = device;
            Reader = reader;
        }

        public MemoryStream Stream { get; set; }
        public Reader Reader { get; set; }
        public Device Device { get; set; }
        public ushort Id { get; set; }
        public ushort Length { get; set; }
        public ushort Version { get; set; }
        public bool Save { get; set; }

        public bool IsServerToClientMessage => Id - 0x4E20 > 0x00;

        public bool IsClientToServerMessage => Id - 0x2710 < 0x2710;

        public void Dispose()
        {
            Stream = null;
            Reader = null;
            Device = null;
        }

        public virtual void Decrypt()
        {
            var buffer = Reader.ReadBytes(Length);

            Device.Rc4.Decrypt(ref buffer);

            Reader = new Reader(buffer);
            Length = (ushort) Reader.BaseStream.Length;
        }

        public virtual void Encrypt()
        {
            var buffer = Stream.ToArray();

            Device.Rc4.Encrypt(ref buffer);

            Stream = new MemoryStream(buffer);
            Length = (ushort) Stream.Length;
        }

        public virtual void Decode()
        {
        }

        public virtual async Task Encode()
        {
        }

        public virtual async Task Process()
        {
        }

        public async Task<byte[]> BuildPacket()
        {
            using (var stream = new MemoryStream())
            {
                Length = (ushort) Stream.Length;

                await stream.WriteUShort(Id);

                stream.WriteByte(0);

                await stream.WriteUShort(Length);
                await stream.WriteUShort(Version);

                await stream.WriteBuffer(Stream.ToArray());

                return stream.ToArray();
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"PACKET ID: {Id}, ");
            builder.Append($"PACKET LENGTH: {Length}, ");
            builder.Append($"PACKET VERSION: {Version}, ");
            builder.Append($"STC: {IsServerToClientMessage}, ");
            builder.Append($"CTS: {IsClientToServerMessage}");

            if (Stream != null)
                builder.AppendLine(
                    $", PACKET PAYLOAD: {BitConverter.ToString(Stream.ToArray()).Replace("-", string.Empty)}");

            return builder.ToString();
        }
    }
}