using SnakeWinUi.Controller;
using SnakeWinUi.UpdateService;
using System.Collections.Generic;

namespace SnakeWinUi.MVVM.Model
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
            this.RegisterAtUpdateGroup();
        }

        private void SetHeadStartingPosition()
        {
            this._startPosition = new Position2D(5,10);  // Do something random later on
            this.Head = this._startPosition;
        }

        private void MoveHead()
        {
            this.Head += this.GetCurrentDirection();
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

        private Position2D GetCurrentDirection() 
        {
            switch (this._curHeadDirection)
            {
                case Direction.Up:
                    return DirectionVector.Up;

                case Direction.Right:
                    return DirectionVector.Right;

                case Direction.Down:
                    return DirectionVector.Down;

                case Direction.Left:
                    return DirectionVector.Left;

                default:
                    return Position2D.Zero;
            }
        }

        public void SetDirection(Direction newDirection)
        {
            this._curHeadDirection = newDirection;
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

        public void RegisterAtUpdateGroup()
        {
            GameManager.Instance.AddToUpdateGroup(this);
        }
    }
}
