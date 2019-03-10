using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RetroClash.Files
{
    public class Levels : IDisposable
    {
        public List<string> NpcLevels = new List<string>();
        public List<string> Prebases = new List<string>();

        public Levels()
        {
            if (Directory.Exists("Assets/") && File.Exists("Assets/starting_home.json"))
                StartingHome = File.ReadAllText("Assets/starting_home.json", Encoding.UTF8);

            NpcLevels.Add(File.ReadAllText("Assets/level/tutorial_npc.json", Encoding.UTF8));
            NpcLevels.Add(File.ReadAllText("Assets/level/tutorial_npc2.json", Encoding.UTF8));

            for (var i = 1; i < 49; i++)
                NpcLevels.Add(File.ReadAllText($"Assets/level/npc{i}.json", Encoding.UTF8));

            for (var i = 1; i < 8; i++)
                Prebases.Add(File.ReadAllText($"Assets/level/townhall{i}.json", Encoding.UTF8));
        }

        public string StartingHome { get; set; }

        public void Dispose()
        {
            StartingHome = null;
            NpcLevels = null;
            Prebases = null;
        }
    }
}