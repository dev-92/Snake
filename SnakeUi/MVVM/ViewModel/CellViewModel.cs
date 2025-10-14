using Microsoft.UI.Xaml.Media;

using SnakeCore.Enums;
using SnakeCore.Model.Entity;
using SnakeCore.Model.ValueObject;
using SnakeUi.Config;
using SnakeUi.Helpers;
using SnakeUi.Utils;

using System.ComponentModel;
using System.Runtime.CompilerServices;

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

    public CellViewModel(CellModel cellModel)
    {
        this.CellModel = cellModel;
        this.CellModel.PropertyChanged += this.CellModelOnPropertyChanged;
        this.UpdateCellBackground();
    }

    private void CellModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(this.CellModel.CellStatus))
        {
            this.UpdateCellBackground();
        }
    }

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

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
