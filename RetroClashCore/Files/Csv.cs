using System;
using System.Collections.Generic;
using RetroClashCore.Files.CsvReader;
using RetroGames.Files.CsvReader;

namespace RetroClashCore.Files
{
    public class Csv : IDisposable
    {
        public static readonly List<Tuple<string, int>> Gamefiles = new List<Tuple<string, int>>();
        public static Gamefiles Tables;

        public Csv()
        {
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/achievements.csv", 1));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/alliance_badges.csv", 2));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/alliance_portal.csv", 3));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/billing_packages.csv", 4));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/building_classes.csv", 5));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/buildings.csv", 6));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/characters.csv", 7));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/decos.csv", 8));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/effects.csv", 9));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/experience_levels.csv", 10));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/faq.csv", 11));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/globals.csv", 12));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/heroes.csv", 13));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/hints.csv", 14));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/leagues.csv", 15));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/locales.csv", 16));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/missions.csv", 17));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/news.csv", 18));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/npcs.csv", 19));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/obstacles.csv", 20));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/particle_emitters.csv", 21));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/projectiles.csv", 22));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/resource_packs.csv", 23));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/resources.csv", 24));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/shields.csv", 25));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/spells.csv", 26));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/texts.csv", 27));
            Gamefiles.Add(new Tuple<string, int>("Assets/csv/traps.csv", 28));

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