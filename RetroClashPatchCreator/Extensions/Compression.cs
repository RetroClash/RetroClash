using System;
using System.IO;
using SevenZip.Sdk;
using SevenZip.Sdk.Compression.Lzma;

namespace RetroClashPatchCreator.Extensions
{
    internal class Compression
    {
        public static void Compress()
        {
            var encoder = new Encoder();
            var files = Directory.GetFiles(Program.SourceDir, "*.*");
            Directory.CreateDirectory(Program.DestinationDir);
            foreach (var file in files)
                using (var uncompressed = new FileStream(file, FileMode.Open))
                {
                    using (var compressed =
                        new FileStream(Path.Combine(Program.DestinationDir, Path.GetFileName(file)), FileMode.Create))
                    {
                        encoder.SetCoderProperties(new[]
                        {
                            CoderPropId.DictionarySize,
                            CoderPropId.PosStateBits,
                            CoderPropId.LitContextBits,
                            CoderPropId.LitPosBits,
                            CoderPropId.Algorithm,
                            CoderPropId.NumFastBytes,
                            CoderPropId.MatchFinder,
                            CoderPropId.EndMarker
                        }, new object[]
                        {
                            262144,
                            2,
                            3,
                            0,
                            2,
                            32,
                            "bt4",
                            false
                        });
                        encoder.WriteCoderProperties(compressed);

                        compressed.Write(BitConverter.GetBytes(uncompressed.Length), 0, 4);

                        encoder.Code(uncompressed, compressed, uncompressed.Length, -1L,
                            null);

                        compressed.Flush();
                        compressed.Close();
                    }

                    uncompressed.Close();
                }
        }
    }
}