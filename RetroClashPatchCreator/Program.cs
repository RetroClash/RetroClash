using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using RetroClashPatchCreator.Extensions;
using SevenZip.Sdk;
using Encoder = SevenZip.Sdk.Compression.Lzma.Encoder;

namespace RetroClashPatchCreator
{
    public class Program
    {
        public static string PatchDir = "../Patch";
        public static string PatchSourceDir = "../Assets";
        public static string PatchDestinationDir = "../Patch/Assets";

        public static string SourceDir = "../Assets/csv";
        public static string DestinationDir = "../Patch/Assets/csv";

        public static void Main(string[] args)
        {
            Console.Title = "RetroClash Patch Creator v1.0";

            Console.WriteLine(
                "\r\n________     _____             ______________             ______  \r\n___  __ \\______  /_______________  ____/__  /_____ __________  /_ \r\n__  /_/ /  _ \\  __/_  ___/  __ \\  /    __  /_  __ `/_  ___/_  __ \\\r\n_  _, _//  __/ /_ _  /   / /_/ / /___  _  / / /_/ /_(__  )_  / / /\r\n/_/ |_| \\___/\\__/ /_/    \\____/\\____/  /_/  \\__,_/ /____/ /_/ /_/ \r\n                                                                  \r\n");

            Console.SetOut(new Prefixed());

            Console.WriteLine("Creating Patch...");

            if (Directory.Exists("../Assets"))
                if (Directory.Exists("../Assets/csv"))
                {
                    Directory.CreateDirectory(DestinationDir);

                    var encoder = new Encoder();

                    foreach (var file in Directory.GetFiles(SourceDir, "*.*"))
                        using (var uncompressed = new FileStream(file, FileMode.Open))
                        {
                            using (var compressed =
                                new FileStream(Path.Combine(DestinationDir, Path.GetFileName(file)), FileMode.Create))
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

                    var fingerprint = string.Empty + "{\"files\":[";

                    using (var sha1 = new SHA1CryptoServiceProvider())
                    {
                        foreach (var path in Directory.GetFiles(PatchDestinationDir, "*.*",
                            SearchOption.AllDirectories))
                            using (var fileStream = new FileStream(path, FileMode.Open))
                            {
                                using (var bufferedStream = new BufferedStream(fileStream))
                                {
                                    fingerprint += "{\"sha\":\"" +
                                                   sha1.ComputeHash(bufferedStream).Aggregate(string.Empty,
                                                       (current, num) => current + num.ToString("x2")) +
                                                   "\",\"file\":\"" +
                                                   Path.Combine(
                                                           new DirectoryInfo(
                                                               Path.GetDirectoryName(path) ??
                                                               throw new InvalidOperationException()).Name,
                                                           Path.GetFileName(path))
                                                       .Replace("\\", "\\/") + "\"},";

                                    bufferedStream.Close();
                                }
                                fileStream.Close();
                            }

                        var version = File.ReadAllText(
                                Path.Combine(PatchSourceDir, "fingerprint.json"))
                            .Split(new[] {"\"version\": \""}, StringSplitOptions.None)[1]
                            .Split(new[] {"\""}, StringSplitOptions.None)[0].Split('.').Select(int.Parse).ToArray();

                        version[2]++;

                        var s = string.Join(".", version.Select(x => x.ToString()).ToArray());

                        var sha = sha1.ComputeHash(Encoding.UTF8.GetBytes(s)).Aggregate(string.Empty,
                            (current, num) => current + num.ToString("x2"));

                        var textWriter = new StreamWriter(Path.Combine(PatchSourceDir, "fingerprint.json"), false);
                        textWriter.Write(fingerprint.TrimEnd(',') + "],\"sha\":\"" + sha + "\",\"version\": \"" + s +
                                         "\"}");
                        textWriter.Close();

                        Directory.CreateDirectory(PatchDir);
                        Directory.Move(PatchDestinationDir, Path.Combine(PatchDir, sha));

                        Console.WriteLine($"Patch {s} -> {sha} has been created.");
                    }
                }
                else
                {
                    Console.WriteLine("../Assets/csv direcory not found. Is this application in the correct folder?");
                }
            else
                Console.WriteLine("../Assets direcory not found. Is this application in the correct folder?");

            Console.ReadKey();
        }
    }
}