using SnakeWinUi.Config;
using SnakeWinUi.MVVM.Model.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeWinUi.MVVM.Model.Entity.Prey
{
    public class Prey
    {
        public int Score { get; set; }
        public Position2D Position { get; set; } = Position2D.Zero;

        public Prey(Position2D position) 
        {
            this.Position = position;
        }   

        public void SpawnPrey(Position2D spawnPosition)
        {
            this.Position = spawnPosition;
        }

    }
}
