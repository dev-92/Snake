using System.Collections.Generic;

namespace SnakeWinUi.Extensions
{
    public static class ListExtensions
    {
        public static bool IsEmpty<T>(this List<T> list)
        {
            return list.Count <= 0;
        }
    }
}
