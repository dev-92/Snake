using Microsoft.UI;
using System;
using Windows.UI; 

namespace SnakeWinUi.Utils
{
    public static class HexColorConverter
    {
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
