namespace SnakeCore.Enums
{
    /// <summary>
    /// Represents the different types of collectable items that can appear in the game.
    /// </summary>
    public enum CollectableItemType
    {
        /// <summary>
        /// Increases or decreases the snake's speed and affects score negatively.
        /// </summary>
        Apple,

        /// <summary>
        /// Adds a small number of tail segments and increases score.
        /// </summary>
        Duck,

        /// <summary>
        /// Reduces score when collected.
        /// </summary>
        Bomb,

        /// <summary>
        /// Increases snake speed and score.
        /// </summary>
        Cherry,

        /// <summary>
        /// Adds one tail segment and increases score slightly.
        /// </summary>
        Mouse,

        /// <summary>
        /// Adds multiple tail segments and increases score significantly.
        /// </summary>
        Rabbit
    }
}
