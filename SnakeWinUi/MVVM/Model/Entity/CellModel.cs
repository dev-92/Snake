using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.Entity.Collectables;
using SnakeWinUi.MVVM.Model.ValueObject;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SnakeWinUi.MVVM.Model.Entity
{
    /// <summary>
    /// Represents a single cell on the game board.
    /// Tracks its position and status (empty, prey, or snake).
    /// Implements <see cref="INotifyPropertyChanged"/> to notify the UI of changes.
    /// </summary>
    public partial class CellModel : INotifyPropertyChanged
    {
        private CollectableItem? _collectableItem {  get; set; }
        public CollectableItem? CollectableItem
        {
            get => this._collectableItem;
            set
            {
                this._collectableItem = value;
                this.CellStatus = value != null ? CellStatus.Collectable : CellStatus.Empty;
            }
        }

        private CellStatus _cellStatus = CellStatus.Empty;
        public CellStatus CellStatus
        {
            get => this._cellStatus;      
            set
            {
                this._cellStatus = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public Position2D Position { get; set; }

        public CellModel(Position2D position)
        {
            this.Position = position;
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event to notify UI of property changes.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed. Automatically provided by compiler if omitted.</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
