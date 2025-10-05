using SnakeWinUi.Controller;
using SnakeWinUi.Services.UpdateService;
using SnakeWinUi.Enums;
using System.Collections.Generic;
using SnakeWinUi.Config;
using System;
using SnakeWinUi.MVVM.Model.ValueObject;

namespace SnakeWinUi.MVVM.Model.Entity
{
    public class SnakeModel : IUpdateEntity
    {
        public Direction CurrentDirection { get; set; } = Direction.Up;
        public Position2D Head { get; set; }
        public List<Position2D> Tail { get; set; }

        private static SnakeModel? _instance;
        public static SnakeModel Instance
        {
            get 
            { 
                if(SnakeModel._instance == null)
                {
                    SnakeModel._instance = new();
                }
                return SnakeModel._instance; 
            }
        }

        private SnakeModel()
        {
            this.Tail = new List<Position2D>();
            this.Head = new Position2D();

            this.SetRandomStartPosition();
            this.SetRandomStartDirection();

            this.RegisterAtUpdateGroup();

            this.Tail.Add(new Position2D(5, 5));
            this.Tail.Add(new Position2D(5, 6));
            this.Tail.Add(new Position2D(5, 7));
        }

        private void SetRandomStartPosition()
        {
            Random random = new Random();

            int xPos = random.Next(0, GameSettings.SideLength - 1);
            int yPos = random.Next(0, GameSettings.SideLength - 1);

            this.Head = new Position2D(xPos, yPos);
        }

        private void SetRandomStartDirection()
        {
            int maxDirectionVariances = 4;

            Random random = new();
            int randomDirectionInt = random.Next(0, maxDirectionVariances);

            switch(randomDirectionInt)
            {
                case (int)Direction.Up:
                    this.CurrentDirection = Direction.Up;
                    break;

                case (int)Direction.Right:
                    this.CurrentDirection = Direction.Right;
                    break;

                case (int)Direction.Down:
                    this.CurrentDirection = Direction.Down;
                    break;

                case (int)Direction.Left:
                    this.CurrentDirection = Direction.Left;
                    break;
            }
        }

        private void MoveHead()
        {
            this.Head += this.GetCurrentDirection();
        }

        private void HandleWallWrapping()
        {
            if (this.Head.Y <= 0 && this.CurrentDirection == Direction.Up)
            {
                this.Head.Y = GameSettings.SideLength;
            }

            if (this.Head.X >= GameSettings.SideLength - 1 && this.CurrentDirection == Direction.Right)
            {
                this.Head.X = - 1;
            }

            if (this.Head.Y >= GameSettings.SideLength - 1 && this.CurrentDirection == Direction.Down)
            {
                this.Head.Y = - 1 ;
            }

            if (this.Head.X <= 0 && this.CurrentDirection == Direction.Left)
            {
                this.Head.X = GameSettings.SideLength;
            }
        }

        private void MoveTail()
        {
            for(int i = 1; i < this.Tail.Count; i++)
            {
                this.Tail[i] = this.Head + this.GetCurrentDirection();
                /*
                int currentPieceIndex = i;
                int previousPieceIndex = i - 1;

                this.Tail[currentPieceIndex] = this.Tail[previousPieceIndex];
                */
            }
        }

        public Position2D GetCurrentDirection() 
        {
            switch (this.CurrentDirection)
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
            this.CurrentDirection = newDirection;
        }

        private void ExtendTail()  // 
        {
            this.Tail.Add(new Position2D(0, 0));
        }

        public void Update()
        {
            this.HandleWallWrapping();

            this.MoveHead();

            this.MoveTail();
        }

        public void RegisterAtUpdateGroup()
        {
            GameManager.Instance.AddToUpdateGroup(this);
        }
    }
}
