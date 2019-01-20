using System.Collections.Generic;
using System.Linq;

namespace RetroGames.Helpers
{
    public static class Extensions
    {
        public static void UpdateOrInsert<T>(this List<T> list, int index, T item)
        {
            if (list.ElementAtOrDefault(index) != null)
                list[index] = item;
            else
                list.Insert(index, item);
        }
    }
}