using RetroClash.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClash.Files.Logic
{
    public class Faq : Data
    {
        public Faq(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string TID { get; set; }

        public string InfoTID { get; set; }
    }
}