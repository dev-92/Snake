using System;

namespace SnakeWinUi.Config
{
    /// <summary>
    /// Holds game values, which can be altered while the runtime is live.
    /// </summary>
    internal static class GameSettings
    {
        private static int _updateSpeedMillis { get; set; } = 100;
        public static int UpdateSpeedMillis
        {
            get => GameSettings._updateSpeedMillis;
            set
            {
                GameSettings._updateSpeedMillis = value;
            }
        }
        
        private static int _cellAmount { get; set; } = 500;
        public static int CellAmount
        {
            get => GameSettings._cellAmount;
            set
            {
                GameSettings._cellAmount = value;
                GameSettings._sideLength = (int)Math.Sqrt(GameSettings._cellAmount);
            }
        }

        private static int _sideLength { get; set; } = (int)Math.Sqrt(GameSettings._cellAmount);
        public static int SideLength => GameSettings._sideLength;





    }
}
