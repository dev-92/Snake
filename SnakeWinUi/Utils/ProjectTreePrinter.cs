using System.Diagnostics;
using System.IO;

namespace SnakeWinUi.Utils
{
    class ProjectTreePrinter
    {

        public static void PrintProjectTree(string path, string indent = "")
        {
            if (!Directory.Exists(path))
            {
                Debug.WriteLine("Path does not exist: " + path);
                return;
            }

            foreach (var dir in Directory.GetDirectories(path))
            {
                Debug.WriteLine(indent + "[D] " + Path.GetFileName(dir));
                ProjectTreePrinter.PrintProjectTree(dir, indent + "    "); 
            }

            foreach (var file in Directory.GetFiles(path))
            {
                Debug.WriteLine(indent + "[F] " + Path.GetFileName(file));
            }
        }
    }
}
