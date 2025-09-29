using Snake.MVVM.Model;
using Snake.UpdateService;
using System.ComponentModel;
using System.Numerics;

namespace Snake.MVVM.ViewModel
{
    internal class CellViewModel
    {
        private CellModel CellModel { get; set; }
        private Border CellVisualization { get; set; }

        private Color EmptyColor { get; } = new Color();
        private Color SnakeColor { get; } = new Color();
        private Color PreyColor { get; } = new Color();

        public CellViewModel(Vector2 cellModelPosition)
        {
            this.CellModel = new CellModel(cellModelPosition);
            this.CellVisualization = new Border();

            this.CellModel.PropertyChanged += this.CellModelOnPropertyChanged;
        }

        private void CellModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CellModel.CellStatus))
            {
                this.UpdateCellVisualizationColor();
            }
        }

        private void UpdateCellVisualizationColor()
        {
            switch (this.CellModel.CellStatus)
            {
                case CellModel.Status.Empty:
                    this.CellVisualization.BackgroundColor = this.EmptyColor;
                    break;

                case CellModel.Status.Snake:
                    this.CellVisualization.BackgroundColor = this.SnakeColor;
                    break;

                case CellModel.Status.Prey:
                    this.CellVisualization.BackgroundColor = this.PreyColor;
                    break;

                default:
                    throw new ArgumentException("Invalid cellstatus");
            }
        }
    }
}
