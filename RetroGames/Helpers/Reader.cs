using System;
using System.IO;
using System.Text;
using RetroGames.Logic;

namespace RetroGames.Helpers
{
    public class Reader : BinaryReader
    {
        public Reader(Stream stream) : base(stream)
        {
        }

        public Reader(byte[] buffer) : base(new MemoryStream(buffer))
        {
        }

        public byte[] ReadAllBytes => ReadBytes((int) BaseStream.Length - (int) BaseStream.Position);

        public override int Read(byte[] buffer, int offset, int count)
        {
            return BaseStream.Read(buffer, offset, count);
        }

        public override bool ReadBoolean()
        {
            var state = ReadByte();
            switch (state)
            {
                case 0:
                    return false;

                case 1:
                    return true;

                default:
                    throw new Exception("Error when reading a bool in a packet.");
            }
        }

        public override byte ReadByte()
        {
            return (byte) BaseStream.ReadByte();
        }

        public byte[] ReadBytes()
        {
            var length = ReadInt32();
            return length == -1 ? null : ReadBytes(length);
        }

        public override short ReadInt16()
        {
            var buffer = ReadBytesWithEndian(2);
            return BitConverter.ToInt16(buffer, 0);
        }

        public int ReadInt24()
        {
            var temp = ReadBytesWithEndian(3, false);
            return (temp[0] << 16) | (temp[1] << 8) | temp[2];
        }

        public override int ReadInt32()
        {
            var buffer = ReadBytesWithEndian(4);
            return BitConverter.ToInt32(buffer, 0);
        }

        public override long ReadInt64()
        {
            var buffer = ReadBytesWithEndian(8);
            return BitConverter.ToInt64(buffer, 0);
        }

        public LogicLong ReadLogicLong()
        {
            return new LogicLong(ReadInt32(), ReadInt32());
        }

        public override string ReadString()
        {
            var length = ReadInt32();

            if (length <= -1 || length > BaseStream.Length - BaseStream.Position || length > 900000)
                return string.Empty;

            var buffer = ReadBytesWithEndian(length, false);
            return Encoding.UTF8.GetString(buffer);
        }

        public override ushort ReadUInt16()
        {
            var buffer = ReadBytesWithEndian(2);
            return BitConverter.ToUInt16(buffer, 0);
        }

        public uint ReadUInt24()
        {
            return (uint) ReadInt24();
        }

        public override uint ReadUInt32()
        {
            var buffer = ReadBytesWithEndian(4);
            return BitConverter.ToUInt32(buffer, 0);
        }

        public override ulong ReadUInt64()
        {
            var buffer = ReadBytesWithEndian(8);
            return BitConverter.ToUInt64(buffer, 0);
        }

        public int ReadVInt()
        {
            var b = ReadByte();
            var v5 = b & 0x80;
            var lr = b & 0x3F;

            if ((b & 0x40) != 0)
            {
                if (v5 == 0) return lr;
                b = ReadByte();
                v5 = ((b << 6) & 0x1FC0) | lr;
                if ((b & 0x80) != 0)
                {
                    b = ReadByte();
                    v5 = v5 | ((b << 13) & 0xFE000);
                    if ((b & 0x80) != 0)
                    {
                        b = ReadByte();
                        v5 = v5 | ((b << 20) & 0x7F00000);
                        if ((b & 0x80) != 0)
                        {
                            b = ReadByte();
                            lr = (int) (v5 | (b << 27) | 0x80000000);
                        }
                        else
                        {
                            lr = (int) (v5 | 0xF8000000);
                        }
                    }
                    else
                    {
                        lr = (int) (v5 | 0xFFF00000);
                    }
                }
                else
                {
                    lr = (int) (v5 | 0xFFFFE000);
                }
            }
            else
            {
                if (v5 == 0) return lr;
                b = ReadByte();
                lr |= (b << 6) & 0x1FC0;
                if ((b & 0x80) == 0) return lr;
                b = ReadByte();
                lr |= (b << 13) & 0xFE000;
                if ((b & 0x80) == 0) return lr;
                b = ReadByte();
                lr |= (b << 20) & 0x7F00000;
                if ((b & 0x80) == 0) return lr;
                b = ReadByte();
                lr |= b << 27;
            }

            return lr;
        }

        public long Seek(long offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        private byte[] ReadBytesWithEndian(int count, bool endian = true)
        {
            var buffer = new byte[count];
            BaseStream.Read(buffer, 0, count);

            if (BitConverter.IsLittleEndian && endian)
                Array.Reverse(buffer);

            return buffer;
        }
    }
}