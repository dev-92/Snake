namespace SnakeWinUi.Services.UpdateService
{
    /// <summary>
    /// Represents an entity that can be updated each game tick.
    /// Provides methods to perform updates and register itself with the update loop.
    /// </summary>
    internal interface IUpdateable
    {
        /// <summary>
        /// Performs the update logic for the entity, called on each game tick.
        /// </summary>
        void Update();

        /// <summary>
        /// Registers this entity with the game's update loop managed by <see cref="GameManager"/>.
        /// </summary>
        void RegisterAtUpdateComposite();
    }
}
