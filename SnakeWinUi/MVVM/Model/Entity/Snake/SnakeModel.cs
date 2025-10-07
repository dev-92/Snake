using SnakeWinUi.Controller;
using SnakeWinUi.Services.UpdateService;
using SnakeWinUi.Enums;
using System.Collections.Generic;
using SnakeWinUi.Config;
using System;
using SnakeWinUi.MVVM.Model.ValueObject;
using SnakeWinUi.Extensions;

namespace SnakeWinUi.MVVM.Model.Entity.Snake
{
    public class SnakeModel : IUpdateEntity
    {
        public Direction CurrentDirection { get; set; } = Direction.Up;
        public SnakeElement Head { get; set; }
        public List<SnakeElement> Tail { get; set; }

        private static SnakeModel? _instance;
        public static SnakeModel Instance
        {
            get 
            { 
                if(_instance == null)
                {
                    _instance = new();
                }
                return _instance; 
            }
        }

        private SnakeModel()
        {
            this.Head = new SnakeElement(this.GetRandomStartPosition());
            this.Tail = new List<SnakeElement>();

            this.SetRandomStartDirection();

            this.RegisterAtUpdateGroup();

            
            this.Tail.Add(new SnakeElement(Position2D.Zero));
            
            this.Tail.Add(new SnakeElement(Position2D.Zero));
            this.Tail.Add(new SnakeElement(Position2D.Zero));
            this.Tail.Add(new SnakeElement(Position2D.Zero));
            this.Tail.Add(new SnakeElement(Position2D.Zero));
            
        }

        private Position2D GetRandomStartPosition()
        {
            Random random = new Random();

            int xPos = random.Next(0, GameSettings.SideLength - 1);
            int yPos = random.Next(0, GameSettings.SideLength - 1);

            //return new Position2D(xPos, yPos);
            return new Position2D(3, 6);
        }

        private void SetRandomStartDirection()
        {
            Random random = new();
            int randomDirectionInt = random.Next(0, Constants.MAX_DIRECTIONS_VARIANCE);

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

            this.CurrentDirection = Direction.Left;
        }

        private void MoveHead()
        {
            this.Head.PreviousPosition = this.Head.CurrentPosition;
            this.Head.CurrentPosition += this.GetCurrentDirection();
        }

        private void HandleWallWrapping()
        {
            if (this.Head.CurrentPosition.X < 0)
            {
                this.Head.CurrentPosition.X += GameSettings.SideLength;
            }

            if (this.Head.CurrentPosition.X >= GameSettings.SideLength)
            {
                this.Head.CurrentPosition.X -= GameSettings.SideLength;
            }

            if (this.Head.CurrentPosition.Y < 0)
            {
                this.Head.CurrentPosition.Y += GameSettings.SideLength;
            }

            if (this.Head.CurrentPosition.Y >= GameSettings.SideLength)
            {
                this.Head.CurrentPosition.Y -= GameSettings.SideLength;
            }

        }

        private void MoveTail()
        {
            if (this.Tail.IsEmpty()) return;

            this.Tail[0].PreviousPosition = this.Tail[0].CurrentPosition;
            this.Tail[0].CurrentPosition = this.Head.PreviousPosition;
    
            for(int i = 0; i < this.Tail.Count - 1; i++)
            {
                this.Tail[i + 1].PreviousPosition = this.Tail[i + 1].CurrentPosition;
                this.Tail[i + 1].CurrentPosition = this.Tail[i].PreviousPosition;
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

        private void ExtendTail()  
        {
            //this.Tail.Add(new Position2D(0, 0));
        }

        public void Update()
        {
            this.MoveHead();

            this.HandleWallWrapping();

            this.MoveTail();
        }

        public void RegisterAtUpdateGroup()
        {
            GameManager.Instance.AddToUpdateGroup(this);
        }
    }
}
