namespace SnakeCore.Enums
{
    /// <summary>
    /// Represents the different sound effects that can be played during the game.
    /// </summary>
    public enum SoundEffectType
    {
        /// <summary>
        /// Played when a cherry is collected.
        /// </summary>
        CherryCollected,

        /// <summary>
        /// Played when a bomb is collected.
        /// </summary>
        BombCollected,

        /// <summary>
        /// Played when an apple is collected.
        /// </summary>
        AppleCollected,

        /// <summary>
        /// Played when a duck is collected.
        /// </summary>
        DuckCollected,

        /// <summary>
        /// Generic fallback for when any collectable item is collected.
        /// </summary>
        CollectedItem
    }

    /// <summary>
    /// Represents the background music tracks that can be played during the game.
    /// </summary>
    public enum GameMusicType
    {
        /// <summary>
        /// The first main game loop music track.
        /// </summary>
        GameLoop,

        /// <summary>
        /// The second main game loop music track.
        /// </summary>
        MenuLoop
    }
}
