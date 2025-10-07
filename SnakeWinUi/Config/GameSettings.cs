using System;

namespace SnakeWinUi.Config
{
    /// <summary>
    /// Holds game values, which can be altered while the runtime is live.
    /// </summary>
    internal static class GameSettings
    {
        private static int _updateSpeedMillis { get; set; } = 100;
        private static int _cellAmount { get; set; } = 1000;
        private static int _sideLength { get; set; } = (int)Math.Sqrt(GameSettings._cellAmount);

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
