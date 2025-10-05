namespace SnakeWinUi.MVVM.Model.ValueObject
{
    internal static class DirectionVector
    {
        public static Position2D Up {  get; set; } = new(0, -1);
        public static Position2D Right {  get; set; } = new(1 , 0);
        public static Position2D Down {  get; set; } = new(0 , 1);
        public static Position2D Left {  get; set; } = new(-1, 0);
    }
}
