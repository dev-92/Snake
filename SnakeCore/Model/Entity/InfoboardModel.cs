using SnakeCore.Config;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SnakeCore.Model.Entity
{
    internal class InfoboardModel
    {
        public const double SPEED_UI_FACTOR = 1.5;

        private double _score { get; set; } = 0;
        public double Score
        {
            get => this._score;
            set
            {
                this._score = Math.Round(value);
                this.OnPropertyChanged(nameof(this.Score));
            }
        }

        private double _speedFactor { get; set; } = 1;
        public double SpeedFactor
        {
            get => this._speedFactor;
            set
            {
                this._speedFactor = Math.Round(value, 2);
                this.OnPropertyChanged(nameof(this.SpeedFactor));
            }
        }

        private int _lengthOfSnake { get; set; } = CoreConstants.INITIAL_SNAKE_LENGTH;
        public int LengthOfSnake
        {
            get => this._lengthOfSnake;
            set
            {
                this._lengthOfSnake = value;
                this.OnPropertyChanged(nameof(this.LengthOfSnake));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public InfoboardModel()
        {

        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event to notify UI of property changes.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed. Automatically provided by compiler if omitted.</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
