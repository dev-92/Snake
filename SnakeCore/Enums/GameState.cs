namespace SnakeCore.Enums
{
    /// <summary>
    /// Represents the current state of the game loop.
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// The game is actively running and updating.
        /// </summary>
        Running,

        /// <summary>
        /// The game is temporarily paused and not updating.
        /// </summary>
        Paused,

        /// <summary>
        /// The game has ended, due to collision.
        /// </summary>
        GameOver
    }
}
