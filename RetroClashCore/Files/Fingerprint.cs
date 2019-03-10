using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace RetroClash.Files
{
    public class Fingerprint : IDisposable
    {
        public Fingerprint()
        {
            Version = new int[3];

            try
            {
                if (File.Exists("Assets/fingerprint.json"))
                {
                    Json = File.ReadAllText("Assets/fingerprint.json");
                    var json = JObject.Parse(Json);
                    Sha = json["sha"].ToObject<string>();
                    Version = json["version"].ToObject<string>().Split('.').Select(int.Parse) as int[];

                    Logger.Log($"Fingerprint v.{json["version"].ToObject<string>()} has been loaded into memory.");
                }
                else
                {
                    Console.WriteLine("The Fingerprint cannot be loaded, the file does not exist.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load the Fingerprint.");
            }
        }

        public string Json { get; set; }

        public string Sha { get; set; }

        public int[] Version { get; set; }

        public int GetMajorVersion => Version?[0] ?? 5;
        public int GetBuildVersion => Version?[1] ?? 2;
        public int GetContentVersion => Version?[2] ?? 4;

        public void Dispose()
        {
            Json = null;
            Sha = null;
            Version = null;
        }
    }
}