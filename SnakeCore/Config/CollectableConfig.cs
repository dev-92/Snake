namespace SnakeCore.Config
{
    /// <summary>
    /// Configuration values for all collectable items in the game.
    /// </summary>
    public static class CollectableConfig
    {
        public const int MAX_ITEMS = 5;

        /// <summary>
        /// Configuration for Apple collectables.
        /// </summary>
        public static class Apple
        {
            /// <summary>Base score awarded for collecting an apple.</summary>
            public const int BASE_SCORE = 2;

            /// <summary>Number of snake parts added when collecting an apple.</summary>
            public const int PARTS_ADDED = 0;

            /// <summary>Lifetime of an apple in milliseconds.</summary>
            public const double APPLE_LIFETIME_MILLIS = 1000;

            /// <summary>Speed factor applied when collecting an apple.</summary>
            public const double APPLE_SPEED_FACTOR = 1.1;
        }

        /// <summary>
        /// Configuration for Duck collectables.
        /// </summary>
        public static class Duck
        {
            /// <summary>Base score awarded for collecting a duck.</summary>
            public const int BASE_SCORE = 2;

            /// <summary>Number of snake parts added when collecting a duck.</summary>
            public const int PARTS_ADDED = 2;

            /// <summary>Lifetime of a duck in milliseconds.</summary>
            public const double Duck_LIFETIME_MILLIS = 2000;
        }

        /// <summary>
        /// Configuration for Bomb collectables.
        /// </summary>
        public static class Bomb
        {
            /// <summary>Base score deducted when collecting a bomb.</summary>
            public const int BASE_SCORE = 10;

            /// <summary>Number of snake parts added when collecting a bomb (none).</summary>
            public const int PARTS_ADDED = 0;

            /// <summary>Lifetime of a bomb in milliseconds.</summary>
            public const double BOMB_LIFETIME_MILLIS = 3000;
        }

        /// <summary>
        /// Configuration for Cherry collectables.
        /// </summary>
        public static class Cherry
        {
            /// <summary>Base score awarded for collecting a cherry.</summary>
            public const int BASE_SCORE = 3;

            /// <summary>Number of snake parts added when collecting a cherry.</summary>
            public const int PARTS_ADDED = 0;

            /// <summary>Lifetime of a cherry in milliseconds.</summary>
            public const double CHERRY_LIFETIME_MILLIS = 4000;

            /// <summary>Speed factor applied when collecting a cherry.</summary>
            public const double CHERRY_SPEED_FACTOR = 0.9;
        }

        /// <summary>
        /// Configuration for Mouse collectables.
        /// </summary>
        public static class Mouse
        {
            /// <summary>Base score awarded for collecting a mouse.</summary>
            public const int BASE_SCORE = 1;

            /// <summary>Number of snake parts added when collecting a mouse.</summary>
            public const int PARTS_ADDED = 1;

            /// <summary>Lifetime of a mouse in milliseconds.</summary>
            public const double MOUSE_LIFETIME_MILLIS = 5000;
        }

        /// <summary>
        /// Configuration for Rabbit collectables.
        /// </summary>
        public static class Rabbit
        {
            /// <summary>Base score awarded for collecting a rabbit.</summary>
            public const int BASE_SCORE = 3;

            /// <summary>Number of snake parts added when collecting a rabbit.</summary>
            public const int PARTS_ADDED = 3;

            /// <summary>Lifetime of a rabbit in milliseconds.</summary>
            public const double RABBIT_LIFETIME_MILLIS = 6000;
        }
    }
}
