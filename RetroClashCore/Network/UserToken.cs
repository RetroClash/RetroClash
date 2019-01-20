using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using RetroClashCore.Logic;

namespace RetroClashCore.Network
{
    public class UserToken : IDisposable
    {
        public SocketAsyncEventArgs EventArgs { get; set; }
        public Device Device { get; set; }
        public MemoryStream Stream { get; set; }
        public Socket Socket { get; set; }

        public void Dispose()
        {
            Device.Dispose();
            Stream.Dispose();

            Device = null;
            Stream = null;
        }

        public void Set(SocketAsyncEventArgs args, Device device)
        {
            Device = device;
            Device.UserToken = this;

            EventArgs = args;
            EventArgs.UserToken = this;

            Socket = EventArgs.AcceptSocket;

            Stream = new MemoryStream();
        }

        public async Task SetData()
        {
            await Stream.WriteAsync(EventArgs.Buffer, 0, EventArgs.BytesTransferred);
        }

        public void Reset()
        {
            Stream.Position = 0;
            Stream.SetLength(0);
        }
    }
}