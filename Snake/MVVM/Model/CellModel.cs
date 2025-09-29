using Snake.UpdateService;
using System.ComponentModel;
using System.Numerics;

namespace Snake.MVVM.Model
{
    internal class CellModel : IUpdateEntity, INotifyPropertyChanged
    {
        public enum Status
        {
            Empty, 
            Prey,
            Snake
        }

        private Status _cellStatus { get; set; } = Status.Empty;
        public Status CellStatus
        {
            get
            {
                return this._cellStatus;
            }
            set
            {
                this._cellStatus = value;
                this.OnPropertyChanged(nameof(CellStatus));
            }
        }

        public Vector2 Position { get; set; }

        public CellModel(Vector2 position) 
        { 
            this.Position = position;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
