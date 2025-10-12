using Microsoft.UI.Xaml.Media;
using SnakeWinUi.Config;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.Entity;
using SnakeWinUi.MVVM.Model.ValueObject;
using SnakeWinUi.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public partial class CellViewModel : INotifyPropertyChanged
{
    public CellModel CellModel { get; set; }

    private SolidColorBrush _emptyColorBrush = new(HexColorConverter.ColorFromHex(Constants.EMPTY_CELL_HEX_COLOR));
    private SolidColorBrush _snakeColorBrush = new(HexColorConverter.ColorFromHex(Constants.SNAKE_HEX_COLOR));

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

    public CellViewModel(Position2D cellModelPosition)
    {
        this.CellModel = new CellModel(cellModelPosition);
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
            CellStatus.Empty            => this._emptyColorBrush,
            CellStatus.Snake            => this._snakeColorBrush,
            CellStatus.Collectable      => this.CellModel.CollectableItem?.ImageBrush,
            _                           => this._emptyColorBrush
        };
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
