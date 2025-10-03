using System;

namespace SnakeWinUi.Config
{
    internal static class GameSettings
    {
        private static int _updateSpeedMillis = 100;
        private static int _cellAmount = 100;
        private static int _sideLength = (int)Math.Sqrt(GameSettings._cellAmount);

        public static int UpdateSpeedMillis => GameSettings._updateSpeedMillis;

        public static int CellAmount
        {
            get
            {
                return GameSettings._cellAmount;
            }
            set
            {
                GameSettings._cellAmount = value;
                GameSettings._sideLength = (int)Math.Sqrt(GameSettings._cellAmount);
            }
        }

        public static int SideLength => GameSettings._sideLength;

    }
}
