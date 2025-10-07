using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using SnakeWinUi.Enums;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SnakeWinUi.MVVM.Model.ValueObject;
using SnakeWinUi.MVVM.Model.Entity;
using Windows.UI.Notifications;

namespace SnakeWinUi.MVVM.ViewModel
{
    public partial class CellViewModel : INotifyPropertyChanged
    {
        public CellModel CellModel { get; set; }

        private SolidColorBrush _emptyColor { get; } = new(Colors.DarkGray);
        private SolidColorBrush _snakeColor { get; } = new(Colors.DarkGreen);
        private SolidColorBrush _preyColor { get; } = new(Colors.DarkRed);

        private SolidColorBrush _backgroundColor {  get; set; } = new(Colors.DarkGray);
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

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CellModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.CellModel.CellStatus))
            {
                this.UpdateBackgroundColor();
            }
        }

        private void UpdateBackgroundColor()
        {
            this.BackgroundColor = this.CellModel.CellStatus switch
            {
                CellStatus.Empty => this._emptyColor,
                CellStatus.Snake => this._snakeColor,
                CellStatus.Prey  => this._preyColor,
                _                => this._emptyColor
            };       
        }
    }
}
