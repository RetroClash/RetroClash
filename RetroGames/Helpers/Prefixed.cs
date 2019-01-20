using System;
using System.IO;
using System.Text;

namespace RetroGames.Helpers
{
    public class Prefixed : TextWriter
    {
        private readonly TextWriter _original;

        public Prefixed()
        {
            _original = Console.Out;
        }

        public override Encoding Encoding => new ASCIIEncoding();

        public override void Write(string text)
        {
            _original.Write(text);
        }

        public override void WriteLine(string text)
        {
            _original.WriteLine($"[{DateTime.Now.ToLongTimeString()}]    {text}");
        }

        public override void Write(char text)
        {
            _original.WriteLine($"[{DateTime.Now.ToLongTimeString()}]    {text}");
        }

        public override void WriteLine()
        {
            _original.WriteLine();
        }
    }
}