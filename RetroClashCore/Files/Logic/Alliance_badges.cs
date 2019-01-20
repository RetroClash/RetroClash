using RetroClashCore.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClashCore.Files.Logic
{
    public class Alliance_badges : Data
    {
        public Alliance_badges(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string IconSWF { get; set; }

        public string IconExportName { get; set; }
    }
}