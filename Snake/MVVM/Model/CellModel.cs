using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Snake.MVVM.Model
{
    public class CellModel : INotifyPropertyChanged
    {
        public enum Status
        {
            Empty, 
            Prey,
            Snake
        }

        private Status _cellStatus = Status.Empty;
        public Status CellStatus
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
