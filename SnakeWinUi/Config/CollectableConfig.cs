
namespace SnakeWinUi.Config
{
    public static class CollectableConfig
    {
        public static class Apple
        {
            public const int BASE_SCORE = -2;
            public const int PARTS_ADDED = 0;

            public const double APPLE_LIFETIME_MILLIS = 1000;
            public const double APPLE_SPEED_FACTOR = 1.1;
        }

        public static class Duck
        {
            public const int BASE_SCORE = 2;
            public const int PARTS_ADDED = 2;

            public const double Duck_LIFETIME_MILLIS = 2000;
        }

        public static class Bomb
        {
            public const int BASE_SCORE = -30;
            public const int PARTS_ADDED = 0;

            public const double BOMB_LIFETIME_MILLIS = 3000;
        }

        public static class Cherry
        {
            public const int BASE_SCORE = +3;
            public const int PARTS_ADDED = 0;

            public const double CHERRY_LIFETIME_MILLIS = 4000;
            public const double CHERRY_SPEED_FACTOR = 0.9;
        }

        public static class Mouse
        {
            public const int BASE_SCORE = 1;
            public const int PARTS_ADDED = 1;

            public const double MOUSE_LIFETIME_MILLIS = 5000;
        }

        public static class Rabbit
        {
            public const int BASE_SCORE = 3;
            public const int PARTS_ADDED = 3;

            public const double RABBIT_LIFETIME_MILLIS = 6000;
        }
    }
}
