using System.Collections.Generic;
using RetroGames.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroRoyale.Files.CsvHelpers
{
    public class DataTable
    {
        protected List<Data> Data;
        protected int Index;

        public DataTable()
        {
            Data = new List<Data>();
        }

        public DataTable(Table table, int index)
        {
            Index = index;
            Data = new List<Data>();

            for (var i = 0; i < table.GetRowCount(); i++)
            {
                var row = table.GetRowAt(i);
                var data = Create(row);

                Data.Add(data);
            }
        }

        public int Count()
        {
            if (Data != null)
                return Data.Count;
            return 0;
        }

        public Data Create(Row row)
        {
            Data data;

            switch (Index)
            {
                default:
                {
                    data = new Data(row, this);
                    break;
                }
            }

            return data;
        }

        public List<Data> GetDatas()
        {
            return Data;
        }

        public Data GetDataWithId(int id)
        {
            return Data[GlobalId.GetInstanceId(id)];
        }

        public Data GetDataWithInstanceId(int id)
        {
            return Data[id];
        }

        public Data GetData(string name)
        {
            return Data.Find(data => data.GetName() == name);
        }

        public int GetIndex()
        {
            return Index;
        }
    }
}