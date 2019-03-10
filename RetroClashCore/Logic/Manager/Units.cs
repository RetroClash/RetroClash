using System.Collections.Generic;
using Newtonsoft.Json;
using RetroClash.Logic.Manager.Items;

namespace RetroClash.Logic.Manager
{
    public class Units
    {
        [JsonProperty("spells")] internal List<Unit> Spells = new List<Unit>();

        [JsonProperty("troops")] internal List<Unit> Troops = new List<Unit>();

        internal void Upgrade(int id, int isSpell)
        {
            if (isSpell == 1)
            {
                var index = Spells.FindIndex(spell => spell.Id == id);

                if (index > -1)
                    Spells[index].Level++;
                else
                    Spells.Add(new Unit
                    {
                        Id = id,
                        Level = 1
                    });
            }
            else
            {
                var index = Troops.FindIndex(troop => troop.Id == id);

                if (index > -1)
                    Troops[index].Level++;
                else
                    Troops.Add(new Unit
                    {
                        Id = id,
                        Level = 1
                    });
            }
        }

        internal void Train(int id, int isSpell, int count)
        {
            if (isSpell == 1)
            {
                var index = Spells.FindIndex(spell => spell.Id == id);

                if (index > -1)
                {
                    if (Spells[index].Count < 30)
                        Spells[index].Count += count;
                }
                else
                {
                    Spells.Add(new Unit
                    {
                        Id = id,
                        Level = 0,
                        Count = count
                    });
                }
            }
            else
            {
                var index = Troops.FindIndex(troop => troop.Id == id);

                if (index > -1)
                {
                    if (Troops[index].Count < 240)
                        Troops[index].Count += count;
                }
                else
                {
                    Troops.Add(new Unit
                    {
                        Id = id,
                        Level = 0,
                        Count = count
                    });
                }
            }
        }
    }
}