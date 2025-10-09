using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWinUi.MVVM.ViewModel
{
    internal class InfoboardViewModel : INotifyPropertyChanged
    {
        private double _score { get; set; } = 0;
        public double Score
        {
            get => this._score;
            set
            {
                this.Score = value;
                this.OnPropertyChanged(nameof(this.Score));
            }
        }

        private double _speedFactor { get; set; } = 1;
        public double SpeedFactor
        {
            get => this._speedFactor;
            set
            {
                this._speedFactor = value;
                this.OnPropertyChanged(nameof(this.SpeedFactor));
            }
        }

        private int _lengthOfSnake { get; set; } = 0;
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

        public InfoboardViewModel()
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
