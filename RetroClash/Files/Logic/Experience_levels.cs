using RetroClash.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClash.Files.Logic
{
    public class Experience_levels : Data
    {
        public Experience_levels(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public int ExpPoints { get; set; }
    }
}