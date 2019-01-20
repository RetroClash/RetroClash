using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroGames.Helpers
{
    public static class Writer
    {
        public static async Task WriteInt(this Stream stream, int value)
        {
            var buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }

        public static void WriteBool(this Stream stream, bool value)
        {
            stream.WriteByte((byte) (value ? 1 : 0));
        }

        public static async Task WriteLong(this Stream stream, long value)
        {
            var buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }

        public static async Task WriteUShort(this Stream stream, ushort value)
        {
            var buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }

        public static async Task WriteBuffer(this Stream stream, byte[] buffer)
        {
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }

        public static async Task WriteString(this Stream stream, string value)
        {
            if (value == null)
            {
                await stream.WriteInt(-1);
            }
            else
            {
                var buffer = Encoding.UTF8.GetBytes(value);

                await stream.WriteInt(buffer.Length);
                await stream.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        public static async Task WriteVInt(this Stream stream, int value)
        {
            await Task.Run(() =>
            {
                if (value > 63)
                {
                    stream.WriteByte((byte)((value & 0x3F) | 0x80));
                    if (value > 8191)
                    {
                        stream.WriteByte((byte)((value >> 6) | 0x80));
                        if (value > 1048575)
                        {
                            stream.WriteByte((byte)((value >> 13) | 0x80));
                            if (value > 134217727)
                            {
                                stream.WriteByte((byte)((value >> 20) | 0x80));
                                value >>= 27 & 0x7F;
                            }
                            else
                            {
                                value >>= 20 & 0x7F;
                            }
                        }
                        else
                        {
                            value >>= 13 & 0x7F;
                        }
                    }
                    else
                    {
                        value >>= 6 & 0x7F;
                    }
                }

                stream.WriteByte((byte)value);
            });
        }

        public static async Task WriteVLong(this Stream stream, long value)
        {
            await stream.WriteVInt(Convert.ToInt32(value >> 32));
            await stream.WriteVInt((int) value);
        }

        public static async Task WriteHex(this Stream stream, string value)
        {
            var tmp = value.Replace("-", string.Empty);
            await stream.WriteBuffer(Enumerable.Range(0, tmp.Length).Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(tmp.Substring(x, 2), 16)).ToArray());
        }
    }
}