using SnakeWinUi.Config;
using SnakeWinUi.MVVM.Model.ValueObject;
using System;

namespace SnakeWinUi.MVVM.Model.Entity.Prey
{
    internal class Prey
    {
        public int Score { get; set; } = 5;
        public Position2D Position { get; set; } = Position2D.Zero;

        public Prey() 
        {

        }   

        public void SetRandomPosition()
        {
            Random random = new();

            int xPos = random.Next(0, GameSettings.SideLength - 1);
            int yPos = random.Next(0, GameSettings.SideLength - 1);

            this.Position = new Position2D(xPos, yPos);
        }

    }
}
