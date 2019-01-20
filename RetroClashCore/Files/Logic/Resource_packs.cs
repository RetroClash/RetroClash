using RetroClashCore.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClashCore.Files.Logic
{
    public class Resource_packs : Data
    {
        public Resource_packs(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string TID { get; set; }

        public string Resource { get; set; }

        public int CapacityPercentage { get; set; }

        public string IconSWF { get; set; }

        public string IconExportName { get; set; }
    }
}