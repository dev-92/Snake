using System.Diagnostics;
using System.IO;

namespace SnakeCore.Utils
{
    /// <summary>
    /// Provides utility methods to print the folder and file structure of a project directory.
    /// </summary>
    class ProjectTreePrinter
    {
        /// <summary>
        /// Recursively prints the directory tree starting from the specified path.
        /// Directories and files are written to the debug output.
        /// </summary>
        /// <param name="path">The root path of the project or directory to print.</param>
        /// <param name="indent">Indentation used for nested directories (optional).</param>
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
