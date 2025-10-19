namespace SnakeUi.Enums
{
    /// <summary>
    /// Represents the current state of the application.
    /// Used to determine which screen or view should be displayed.
    /// </summary>
    public enum AppState
    {
        /// <summary>
        /// The main menu is displayed.
        /// </summary>
        MainMenu,

        /// <summary>
        /// The game is currently being played.
        /// </summary>
        Playing,

        /// <summary>
        /// The game has ended and the Game Over screen is displayed.
        /// </summary>
        GameOver,

        /// <summary>
        /// The settings menu is displayed.
        /// </summary>
        Settings
    }
}
