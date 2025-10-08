using Microsoft.UI.Xaml.Media;

using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.Entity;
using SnakeWinUi.MVVM.Model.ValueObject;
using SnakeWinUi.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SnakeWinUi.MVVM.ViewModel
{
    /// <summary>
    /// Represents a single cell on the game board and manages its visual representation.
    /// Updates its background color based on the current <see cref="CellModel.CellStatus"/>.
    /// Implements <see cref="INotifyPropertyChanged"/> for UI binding.
    /// </summary>
    public partial class CellViewModel : INotifyPropertyChanged
    {
        
        public CellModel CellModel { get; set; }

        private SolidColorBrush _emptyColor { get; } = new(HexColorConverter.ColorFromHex(Config.Constants.EMPTY_CELL_HEX_COLOR));
        private SolidColorBrush _snakeColor { get; } = new(HexColorConverter.ColorFromHex(Config.Constants.SNAKE_HEX_COLOR));

        private SolidColorBrush _backgroundColor { get; set; } = new(HexColorConverter.ColorFromHex(Config.Constants.EMPTY_CELL_HEX_COLOR));
        public SolidColorBrush BackgroundColor
        {
            get
            {
                return this._backgroundColor;
            }
            set
            {
                this._backgroundColor = value;
                this.OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public CellViewModel(Position2D cellModelPosition)
        {
            this.CellModel = new(cellModelPosition);
            this.CellModel.PropertyChanged += this.CellModelOnPropertyChanged;
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for the specified property.
        /// </summary>
        /// <param name="propertyName">The property name that changed. Automatically provided by the compiler.</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Handles changes from the <see cref="CellModel"/> and updates the background color if necessary.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The property change arguments.</param>
        private void CellModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.CellModel.CellStatus))
            {
                this.UpdateBackgroundColor();
            }
        }

        /// <summary>
        /// Updates the background color of the cell based on its current <see cref="CellModel.CellStatus"/>.
        /// </summary>
        private void UpdateBackgroundColor()
        {
            this.BackgroundColor = this.CellModel.CellStatus switch
            {
                CellStatus.Empty => this._emptyColor,
                CellStatus.Snake => this._snakeColor,
                _                => this._emptyColor
            };
        }

    }
}
