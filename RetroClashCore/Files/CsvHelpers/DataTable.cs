using System.Collections.Generic;
using RetroClash.Files.Logic;
using RetroGames.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClash.Files.CsvHelpers
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
                case 1:
                {
                    data = new Achievements(row, this);
                    break;
                }

                case 2:
                {
                    data = new Alliance_badges(row, this);
                    break;
                }

                case 3:
                {
                    data = new Alliance_portal(row, this);
                    break;
                }

                case 4:
                {
                    data = new Billing_packages(row, this);
                    break;
                }

                case 5:
                {
                    data = new Building_classes(row, this);
                    break;
                }

                case 6:
                {
                    data = new Buildings(row, this);
                    break;
                }

                case 7:
                {
                    data = new Characters(row, this);
                    break;
                }

                case 8:
                {
                    data = new Decos(row, this);
                    break;
                }

                case 9:
                {
                    data = new Effects(row, this);
                    break;
                }

                case 10:
                {
                    data = new Experience_levels(row, this);
                    break;
                }

                case 11:
                {
                    data = new Faq(row, this);
                    break;
                }

                case 12:
                {
                    data = new Globals(row, this);
                    break;
                }

                case 13:
                {
                    data = new Heroes(row, this);
                    break;
                }

                case 14:
                {
                    data = new Hints(row, this);
                    break;
                }

                case 15:
                {
                    data = new Leagues(row, this);
                    break;
                }

                case 16:
                {
                    data = new Locales(row, this);
                    break;
                }

                case 17:
                {
                    data = new Missions(row, this);
                    break;
                }

                case 18:
                {
                    data = new News(row, this);
                    break;
                }

                case 19:
                {
                    data = new Npcs(row, this);
                    break;
                }

                case 20:
                {
                    data = new Obstacles(row, this);
                    break;
                }

                case 21:
                {
                    data = new Particle_emitters(row, this);
                    break;
                }

                case 22:
                {
                    data = new Projectiles(row, this);
                    break;
                }

                case 23:
                {
                    data = new Resource_packs(row, this);
                    break;
                }

                case 24:
                {
                    data = new Logic.Resources(row, this);
                    break;
                }

                case 25:
                {
                    data = new Shields(row, this);
                    break;
                }

                case 26:
                {
                    data = new Spells(row, this);
                    break;
                }

                case 27:
                {
                    data = new Texts(row, this);
                    break;
                }

                case 28:
                {
                    data = new Traps(row, this);
                    break;
                }

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