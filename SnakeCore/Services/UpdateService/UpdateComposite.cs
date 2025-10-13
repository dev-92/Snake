
namespace SnakeCore.Services.UpdateService
{
    /// <summary>
    /// Manages a collection of <see cref="IUpdateable"/> objects, allowing them to be updated simultaneously.
    /// Acts as a composite for game entities that require periodic updates.
    /// </summary>
    internal class UpdateComposite
    {
        private List<IUpdateable> _updateEntities { get; set; } = new List<IUpdateable>();

        /// <summary>
        /// Adds an entity to the update group so it will be included in subsequent updates.
        /// </summary>
        /// <param name="updateEntity">The entity to add to the group.</param>
        public void AddParticipant(IUpdateable updateEntity)
        {
            this._updateEntities.Add(updateEntity);
        }

        /// <summary>
        /// Calls the <see cref="IUpdateable.Update"/> method on all registered entities.
        /// </summary>
        public void Update()
        {
            foreach (IUpdateable updateParticipant in this._updateEntities)
            {
                updateParticipant.Update();
            }
        }
    }
}
