using System.Collections.Generic;
using RetroRoyale.Logic.Manager.Items;

namespace RetroRoyale.Logic.Manager
{
    public class Achievements : List<Achievement>
    {
        public new void Add(Achievement achievement)
        {
            if (!Contains(achievement))
                base.Add(achievement);
        }
    }
}