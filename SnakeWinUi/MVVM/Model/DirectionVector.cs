

namespace SnakeWinUi.MVVM.Model
{
    internal static class DirectionVector
    {
        public static Position2D Up {  get; set; } = new Position2D(-1 , 0);
        public static Position2D Right {  get; set; } = new Position2D(0 , 1);
        public static Position2D Down {  get; set; } = new Position2D(1 , 0);
        public static Position2D Left {  get; set; } = new Position2D(0, -1);
    }
}
