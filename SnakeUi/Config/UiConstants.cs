
namespace SnakeUi.Config
{
    public static class UiConstants
    {
        public static readonly int WINDOW_HEIGHT = 1200;
        public static readonly int WINDOW_WIDTH = (int)(UiConstants.WINDOW_HEIGHT * 0.9);

        public static readonly int INFOBOARD_HEIGHT = (int)(UiConstants.WINDOW_HEIGHT * 0.05);
        public static readonly int GAMEBOARD_SIZE = (int)(UiConstants.WINDOW_HEIGHT * 0.95);

        public const string WINDOW_TITLE = "Snake";
        public const string PATH_TO_SNAKE_ICON = @"ms-appx:///Assets/snake_icon.ico";

        public const double BORDER_THICKNESS = 0;
        public const double CORNER_RADIUS = 7;
        public const double BORDER_MARGIN = 0.3;
    }
}