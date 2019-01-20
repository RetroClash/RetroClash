using System;
using System.Collections.Generic;
using RetroRoyale.Files.CsvReader;
using RetroGames.Files.CsvReader;

namespace RetroRoyale.Files
{
    public class Csv : IDisposable
    {
        public static readonly List<Tuple<string, int>> Gamefiles = new List<Tuple<string, int>>();
        public static Gamefiles Tables;

        public Csv()
        {
            Tables = new Gamefiles();

            foreach (var file in Gamefiles)
                Tables.Initialize(new Table(file.Item1), file.Item2);

            Logger.Log($"Succesfully loaded {Gamefiles.Count} Gamefiles into memory.");
        }

        public void Dispose()
        {
            Gamefiles.Clear();
            Tables.Dispose();
        }
    }
}