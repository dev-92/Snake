
using System.Collections.Generic;

namespace SnakeWinUi.UpdateService
{
    /// <summary>
    /// Creates a group where entities can be added. Entities can be updated simultaneously.
    /// </summary>
    internal class UpdateGroup
    {
        private List<IUpdateEntity> _updateEntities = new List<IUpdateEntity>();

        /// <summary>
        /// Adds a participant to the update group
        /// </summary>
        /// <param name="updateEntity">Entity to be added to composite</param>
        public void AddParticipant(IUpdateEntity updateEntity)
        {
            this._updateEntities.Add(updateEntity);   
        }

        /// <summary>
        /// Let's each entity call it's own update method
        /// </summary>
        public void Update()
        {
            foreach(IUpdateEntity updateParticipant in this._updateEntities)
            {
                updateParticipant.Update();
            }
        }

    }
}
