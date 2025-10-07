namespace SnakeWinUi.MVVM.Model.ValueObject
{
    /// <summary>
    /// Provides predefined direction vectors used for snake movement.
    /// Each vector represents a unit step in a specific direction.
    /// </summary>
    internal static class DirectionVector
    {
        /// <summary>
        /// Represents a unit vector pointing upward (0, -1).
        /// </summary>
        public static Position2D Up { get; set; } = new(0, -1);

        /// <summary>
        /// Represents a unit vector pointing to the right (1, 0).
        /// </summary>
        public static Position2D Right { get; set; } = new(1, 0);

        /// <summary>
        /// Represents a unit vector pointing downward (0, 1).
        /// </summary>
        public static Position2D Down { get; set; } = new(0, 1);

        /// <summary>
        /// Represents a unit vector pointing to the left (-1, 0).
        /// </summary>
        public static Position2D Left { get; set; } = new(-1, 0);
    }
}
