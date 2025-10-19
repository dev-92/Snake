using Windows.ApplicationModel.Contacts;

namespace SnakeCore.Config
{
    /// <summary>
    /// Holds core constant values used throughout the game.
    /// </summary>
    public static class CoreConstants
    {
        /// <summary>
        /// The starting length of the snake at the beginning of the game.
        /// </summary>
        public const int INITIAL_SNAKE_LENGTH = 3;

        /// <summary>
        /// Maximum number of consecutive direction changes allowed for the snake.
        /// </summary>
        public const int MAX_DIRECTIONS_VARIANCE = 4;

        /// <summary>
        /// Basic time between two update steps
        /// </summary>
        public const int BASIC_UPDATE_MILLIS = 100;
    }
}
