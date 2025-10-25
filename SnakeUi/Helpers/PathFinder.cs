using System;
using System.IO;

namespace SnakeUi.Helpers
{
    internal static class PathFinder
    {
        public static string GetAbsolutePath(string relativePath)
        {
            return Path.Combine(AppContext.BaseDirectory, relativePath.Replace('/', '\\'));
        }
    }
}
