using Snake.MVVM.Model;
using Snake.MVVM.ViewModel;
using Snake.UpdateService;

namespace Snake.MVVM.View;

public partial class GameboardView : ContentView , IUpdateEntity
{
    private const int CELL_HEIGHT = 50;
    private const int CELL_WIDTH = 50;

    private int _cellAmount {  get; set; }
    public int SideLength { get; set; }

	private Color _strokeColor { get; set; } = Colors.Pink;
	private const double STROKE_THICKNESS  = 1;

	public List<CellViewModel> CellViewModels { get; set; } = new List<CellViewModel>();
    public Dictionary<Position2D, CellModel> CellLookup { get; set; } = new Dictionary<Position2D, CellModel>();

    public GameboardView(int cellAmount)
	{
        this.InitializeComponent();

        this._cellAmount = cellAmount;
		this.SetSideLength();

		this.BuildGameboardStructure();
		this.BuildGameboardGrid();

		this.BuildBorderElements();
    }

	private void BuildGameboardStructure()
	{
		for (int row = 0; row < this.SideLength; row++)
		{
			for (int col = 0; col < this.SideLength; col++)
			{
				Position2D position = new Position2D(row, col);
                this.CellViewModels.Add(new CellViewModel(position));

                CellModel cellModel = new CellModel(position);
                this.CellLookup[position] = cellModel;
			}
		}
	}

	private void BuildGameboardGrid()
	{
        for (int i = 0; i < this.SideLength; i++)
        {
            this.GameboardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(GameboardView.CELL_HEIGHT)});
            this.GameboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(GameboardView.CELL_WIDTH) });
        }
    }

	private void BuildBorderElements()
	{
		foreach(CellViewModel cellViewModel in this.CellViewModels)
		{
			Border border = new Border
			{
				Stroke = this._strokeColor,
				StrokeThickness = GameboardView.STROKE_THICKNESS				
			};

			border.SetBinding(Border.BackgroundColorProperty, nameof(CellViewModel.BackgroundColor));
			border.BindingContext = cellViewModel;

			int borderPosX = (int)cellViewModel.CellModel.Position.X;
			int borderPosY = (int)cellViewModel.CellModel.Position.Y;		

            Grid.SetRow(border, borderPosX);
			Grid.SetColumn(border, borderPosY);

			this.GameboardGrid.Children.Add(border);
		}
	}

    private void SetSideLength()
	{
		this.SideLength = (int)Math.Sqrt(this._cellAmount);
	}

	private void ClearBoard()
	{
		foreach(CellViewModel cellViewModel in this.CellViewModels)
		{
            cellViewModel.CellModel.CellStatus = CellModel.Status.Empty;
        }
	}

    public void Update()
    {
		this.ClearBoard();
    }
}