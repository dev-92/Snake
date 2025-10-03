using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using SnakeWinUi.MVVM.Model;
using SnakeWinUi.Enums;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;

namespace SnakeWinUi.MVVM.ViewModel
{
    public class CellViewModel : INotifyPropertyChanged
    {
        public CellModel CellModel { get; set; }

        private SolidColorBrush _backgroundColor {  get; set; } = new SolidColorBrush(Colors.White);
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

        private SolidColorBrush EmptyColor { get; } = new SolidColorBrush(Color.FromArgb(255, 39, 174, 96));
        private SolidColorBrush SnakeColor { get; } = new SolidColorBrush(Color.FromArgb(255, 192, 57, 43));
        private SolidColorBrush PreyColor { get; } = new SolidColorBrush(Color.FromArgb(255, 127, 140, 141));

        public CellViewModel(Position2D cellModelPosition)
        {
            this.CellModel = new CellModel(cellModelPosition);
            this.CellModel.PropertyChanged += this.CellModelOnPropertyChanged;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CellModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CellModel.CellStatus))
            {
                this.UpdateBackgroundColor();
            }
        }

        private void UpdateBackgroundColor()
        {
            switch (this.CellModel.CellStatus)
            {
                case CellStatus.Empty:
                    this.BackgroundColor = this.EmptyColor;
                    break;

                case CellStatus.Snake:
                    this.BackgroundColor = this.SnakeColor;
                    break;

                case CellStatus.Prey:
                    this.BackgroundColor = this.PreyColor;
                    break;

                default:
                    throw new ArgumentException("Invalid cellstatus");
            }
        }
    }
}
