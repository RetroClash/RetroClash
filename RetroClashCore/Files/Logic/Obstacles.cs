using RetroClash.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClash.Files.Logic
{
    public class Obstacles : Data
    {
        public Obstacles(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string TID { get; set; }

        public string SWF { get; set; }

        public string ExportName { get; set; }

        public string ExportNameBase { get; set; }

        public string ExportNameBaseNpc { get; set; }

        public int ClearTimeSeconds { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Resource { get; set; }

        public bool Passable { get; set; }

        public string ClearResource { get; set; }

        public int ClearCost { get; set; }

        public string LootResource { get; set; }

        public int LootCount { get; set; }

        public string ClearEffect { get; set; }

        public string PickUpEffect { get; set; }

        public int RespawnWeight { get; set; }

        public bool IsTombstone { get; set; }
    }
}