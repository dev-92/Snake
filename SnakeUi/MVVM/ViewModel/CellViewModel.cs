using Microsoft.UI.Xaml.Media;

using SnakeCore.Enums;
using SnakeCore.Model.Entity;
using SnakeUi.Config;
using SnakeUi.Helpers;
using SnakeUi.Utils;

using System.ComponentModel;
using System.Runtime.CompilerServices;

/// <summary>
/// Represents the view model for a single cell on the game board.
/// Handles property changes and updates the visual background based on the cell status.
/// </summary>
public partial class CellViewModel : INotifyPropertyChanged
{
    public CellModel CellModel { get; set; }

    private SolidColorBrush _emptyColorBrush = new(HexColorConverter.ColorFromHex(UiConstants.EMPTY_CELL_HEX_COLOR));
    private SolidColorBrush _snakeColorBrush = new(HexColorConverter.ColorFromHex(UiConstants.SNAKE_HEX_COLOR));

    private Brush? _backgroundBrush { get; set; }
    public Brush? BackgroundBrush
    {
        get => this._backgroundBrush;
        set
        {
            this._backgroundBrush = value;
            this.OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Initializes a new instance of <see cref="CellViewModel"/> with the specified <see cref="CellModel"/>.
    /// Subscribes to property changes to update the background accordingly.
    /// </summary>
    public CellViewModel(CellModel cellModel)
    {
        this.CellModel = cellModel;
        this.CellModel.PropertyChanged += this.CellModelOnPropertyChanged;
        this.UpdateCellBackground();
    }

    /// <summary>
    /// Handles property changes from the underlying <see cref="CellModel"/>.
    /// Updates the background when the cell status changes.
    /// </summary>
    private void CellModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(this.CellModel.CellStatus))
        {
            this.UpdateCellBackground();
        }
    }

    /// <summary>
    /// Updates the background brush based on the current cell status.
    /// Uses different colors or image brushes depending on the cell content.
    /// </summary>
    private void UpdateCellBackground()
    {               
        this.BackgroundBrush = this.CellModel.CellStatus switch
        {
            CellStatus.Empty        => this._emptyColorBrush,
            CellStatus.Snake        => this._snakeColorBrush,
            CellStatus.Collectable  => CollectableItemBrushHelper.GetImageBrush(this.CellModel.CollectableItem),
            _                       => this._emptyColorBrush
        };           
        
    }

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event to notify the UI of property updates.
    /// </summary>
    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
