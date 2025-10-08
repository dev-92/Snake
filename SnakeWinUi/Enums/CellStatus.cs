
namespace SnakeWinUi.Enums
{
    /// <summary>
    /// Represents the current status of a cell on the game board.
    /// </summary>
    public enum CellStatus
    {
        /// <summary>
        /// The cell is empty and contains nothing.
        /// </summary>
        Empty,

        /// <summary>
        /// The cell contains a prey (food) that the snake can eat.
        /// </summary>
        Collectable,

        /// <summary>
        /// The cell is occupied by part of the snake.
        /// </summary>
        Snake
    }
}
