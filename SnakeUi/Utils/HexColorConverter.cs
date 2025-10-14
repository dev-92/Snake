using Microsoft.UI;
using System;
using Windows.UI;

namespace SnakeUi.Utils
{
    /// <summary>
    /// Provides helper methods to convert hexadecimal color strings to <see cref="Color"/> objects.
    /// </summary>
    public static class HexColorConverter
    {
        /// <summary>
        /// Converts a hexadecimal color string (RGB or ARGB) to a <see cref="Color"/> instance.
        /// </summary>
        /// <param name="hex">The hexadecimal color string, optionally starting with '#'.</param>
        /// <returns>A <see cref="Color"/> object representing the specified hex color.</returns>
        public static Color ColorFromHex(string hex)
        {
            hex = hex.Replace("#", "");
            byte a = 255;

            if (hex.Length == 8) // optional ARGB
            {
                a = Convert.ToByte(hex.Substring(0, 2), 16);
                hex = hex.Substring(2);
            }

            byte r = Convert.ToByte(hex.Substring(0, 2), 16);
            byte g = Convert.ToByte(hex.Substring(2, 2), 16);
            byte b = Convert.ToByte(hex.Substring(4, 2), 16);

            return ColorHelper.FromArgb(a, r, g, b);
        }
    }
}
