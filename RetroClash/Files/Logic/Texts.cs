using RetroClash.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClash.Files.Logic
{
    public class Texts : Data
    {
        public Texts(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string TID { get; set; }

        public string EN { get; set; }

        public string FR { get; set; }

        public string DE { get; set; }

        public string ES { get; set; }

        public string IT { get; set; }

        public string NL { get; set; }

        public string NO { get; set; }

        public string PT { get; set; }

        public string TR { get; set; }

        public string JP { get; set; }

        public string CN { get; set; }
    }
}