using RetroClashCore.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClashCore.Files.Logic
{
    public class Alliance_portal : Data
    {
        public Alliance_portal(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string TID { get; set; }

        public string SWF { get; set; }

        public string ExportName { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}