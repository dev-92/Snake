using SnakeWinUi.MVVM.Model.ValueObject;

namespace SnakeWinUi.MVVM.Model.Entity.Snake
{
    public class SnakeElement
    {
        public Position2D CurrentPosition { get; set; }
        public Position2D PreviousPosition { get; set; } = Position2D.Zero;

        public SnakeElement(Position2D currentPosition)
        {
            this.CurrentPosition = currentPosition;
        }

    }
}
