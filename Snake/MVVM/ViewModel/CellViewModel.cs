using Snake.MVVM.Model;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Snake.MVVM.ViewModel
{
    public class CellViewModel : INotifyPropertyChanged
    {
        public CellModel CellModel { get; set; }

        private Color _backgroundColor {  get; set; } = Colors.White;
        public Color BackgroundColor
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

        private Color EmptyColor { get; } = Colors.White;
        private Color SnakeColor { get; } = Colors.Green;
        private Color PreyColor { get; } = Colors.Red;

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
                case CellModel.Status.Empty:
                    this.BackgroundColor = this.EmptyColor;
                    break;

                case CellModel.Status.Snake:
                    this.BackgroundColor = this.SnakeColor;
                    break;

                case CellModel.Status.Prey:
                    this.BackgroundColor = this.PreyColor;
                    break;

                default:
                    throw new ArgumentException("Invalid cellstatus");
            }
        }
    }
}
