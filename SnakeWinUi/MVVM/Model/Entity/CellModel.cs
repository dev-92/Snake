using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.ValueObject;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SnakeWinUi.MVVM.Model.Entity
{
    public class CellModel : INotifyPropertyChanged
    {
        private CellStatus _cellStatus = CellStatus.Empty;
        public CellStatus CellStatus
        {
            get
            {
                return this._cellStatus;
            }
            set
            {
                this._cellStatus = value;
                this.OnPropertyChanged();
            }
        }

        public Position2D Position { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public CellModel(Position2D position) 
        { 
            this.Position = position;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
