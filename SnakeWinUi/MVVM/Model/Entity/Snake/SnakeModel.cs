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

            return new Position2D(xPos, yPos);
        }

        private void SetRandomStartDirection()
        {
            int randomDirectionInt = new Random().Next(0, Constants.MAX_DIRECTIONS_VARIANCE);

            this.CurrentDirection = randomDirectionInt switch
            {
                (int)Direction.Up    => Direction.Up,
                (int)Direction.Right => Direction.Right,
                (int)Direction.Down  => Direction.Down,
                (int)Direction.Left  => Direction.Left,
                _                    => Direction.Up,
            };
        }

        private void MoveHead()
        {
            this.Head.PreviousPosition = this.Head.CurrentPosition;
            this.Head.CurrentPosition += this.GetCurrentDirection();
        }

        public Position2D GetCurrentDirection()
        {
            return this.CurrentDirection switch
            {
                Direction.Up    => DirectionVector.Up,
                Direction.Right => DirectionVector.Right,
                Direction.Down  => DirectionVector.Down,
                Direction.Left  => DirectionVector.Left,
                _               => Position2D.Zero
            };
        }

        public void SetDirection(Direction newDirection)
        {
            if (this.IsOppositeOfCurrentDirection(newDirection)) return;

            this.CurrentDirection = newDirection;
        }

        private bool IsOppositeOfCurrentDirection(Direction newDirection)
        {
            if (this.CurrentDirection == Direction.Up && newDirection == Direction.Down) return true;

            if (this.CurrentDirection == Direction.Right && newDirection == Direction.Left) return true;

            if (this.CurrentDirection == Direction.Down && newDirection == Direction.Up) return true;

            if (this.CurrentDirection == Direction.Left && newDirection == Direction.Right) return true;

            return false;
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
