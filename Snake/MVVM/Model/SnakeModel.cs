using Snake.UpdateService;

namespace Snake.MVVM.Model
{
    public class SnakeModel : IUpdateEntity
    {
        public enum Direction
        {
            Up, 
            Right,
            Down, 
            Left,
        }
        private Direction _curHeadDirection { get; set; } = Direction.Up;

        public Position2D Head { get; set; }
        private Position2D _startPosition { get; set; }
        public List<Position2D> Tail { get; set; }


        private static SnakeModel? _instance;
        public static SnakeModel Instance
        {
            get 
            { 
                if(SnakeModel._instance == null)
                {
                    SnakeModel._instance = new SnakeModel();
                }
                return SnakeModel._instance; 
            }
        }

        private SnakeModel()
        {
            this.Tail = new List<Position2D>();
            this.Head = new Position2D();

            this.SetHeadStartingPosition();
        }

        private void SetHeadStartingPosition()
        {
            this._startPosition = new Position2D(5,5);  // Do something random later on
            this.Head = this._startPosition;
        }

        private void MoveHead()
        {
            this.Head += this.GetCurrentDirectionVector();
        }

        private void MoveTail()
        {
            for(int i = 1; i < this.Tail.Count; i++)
            {
                int currentPieceIndex = i;
                int previousPieceIndex = i - 1;

                this.Tail[currentPieceIndex] = this.Tail[previousPieceIndex];
            }
        }

        private Position2D GetCurrentDirectionVector()
        {
            switch (this._curHeadDirection)
            {
                case Direction.Up:
                    return new Position2D(0, 1);

                case Direction.Right:
                    return new Position2D(1, 0);

                case Direction.Down:
                    return new Position2D(0, -1);

                case Direction.Left:
                    return new Position2D(-1, 0);

                default:
                    return Position2D.Zero;
            }
        }

        private void SetCurrentDirection()
        {
            // check if direction is possible
        }

        private void ExtendTail()  // 
        {
            this.Tail.Add(new Position2D(0, 0));
        }

        public void Update()
        {
            this.MoveHead();
            this.MoveTail();
        }
            
    }
}
