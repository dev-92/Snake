using System.Collections.Generic;

namespace SnakeCore.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="List{T}"/> to simplify common operations.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Determines whether the list contains no elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <returns><c>true</c> if the list is empty; otherwise, <c>false</c>.</returns>
        public static bool IsEmpty<T>(this List<T> list)
        {
            return list.Count <= 0;
        }
    }
}
