using Snake.UpdateService;
using System.Numerics;

namespace Snake.MVVM.Model
{
    internal class SnakeModel : IUpdateEntity
    {
        public enum Direction
        {
            Up, 
            Right,
            Down, 
            Left,
        }
        private Direction _curHeadDirection { get; set; } = Direction.Up;

        private Vector2 _head { get; set; }
        private Vector2 _startPosition { get; set; }
        private List<Vector2> _tail { get; set; }


        private static SnakeModel? _instance;
        public static SnakeModel Instance
        {
            get 
            { 
                if(_instance == null)
                {
                    _instance = new SnakeModel();
                }
                return _instance; 
            }
        }

        private SnakeModel()
        {
            this._tail = new List<Vector2>();
            this._head = new Vector2();

            this.SetHeadStartingPosition();
        }

        private void SetHeadStartingPosition()
        {
            this._startPosition = Vector2.Zero;  // Do something random later on
            this._head = this._startPosition;
        }

        private void MoveHead()
        {
            this._head += this.GetCurrentDirectionVector();
        }

        private void MoveTail()
        {
            for(int i = 0; i < this._tail.Count - 1; i++)
            {
                this._tail[i + 1] = this._tail[i];
            }
        }

        private Vector2 GetCurrentDirectionVector()
        {
            switch (this._curHeadDirection)
            {
                case Direction.Up:
                    return new Vector2(0, 1);

                case Direction.Right:
                    return new Vector2(1, 0);

                case Direction.Down:
                    return new Vector2(0, -1);

                case Direction.Left:
                    return new Vector2(-1, 0);

                default:
                    return Vector2.Zero;
            }
        }

        private void SetCurrentDirection()
        {
            // check if direction is possible

        }

        private void ExtendTail()
        {
            this._tail.Add(new Vector2(0, 0));
        }

        public void Update()
        {
            this.MoveHead();
            this.MoveTail();
        }
            
    }
}
