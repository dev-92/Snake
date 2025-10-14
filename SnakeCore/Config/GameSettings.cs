namespace SnakeCore.Config
{
    /// <summary>
    /// Holds mutable game values that can be adjusted at runtime.
    /// </summary>
    public static class GameSettings
    {
        private static int _updateSpeedMillis { get; set; } = 100;

        /// <summary>
        /// The time interval in milliseconds between game updates.
        /// Can be modified during gameplay to speed up or slow down the game.
        /// </summary>
        public static int UpdateSpeedMillis
        {
            get => GameSettings._updateSpeedMillis;
            set => GameSettings._updateSpeedMillis = value;
        }

        private static int _cellAmount { get; set; } = 500;

        /// <summary>
        /// The total number of cells on the game board.
        /// Changing this value also updates <see cref="SideLength"/> automatically.
        /// </summary>
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

        /// <summary>
        /// The number of cells along one side of the square game board.
        /// Calculated automatically based on <see cref="CellAmount"/>.
        /// </summary>
        public static int SideLength => GameSettings._sideLength;
    }
}
