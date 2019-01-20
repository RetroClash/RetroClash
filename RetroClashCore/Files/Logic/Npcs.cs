using RetroClashCore.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClashCore.Files.Logic
{
    public class Npcs : Data
    {
        public Npcs(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string MapInstanceName { get; set; }

        public string MapDependencies { get; set; }

        public string TID { get; set; }

        public int ExpLevel { get; set; }

        public string UnitType { get; set; }

        public int UnitCount { get; set; }

        public string LevelFile { get; set; }

        public int Gold { get; set; }

        public int Elixir { get; set; }

        public bool AlwaysUnlocked { get; set; }
    }
}