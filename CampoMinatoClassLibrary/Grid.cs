namespace CampoMinatoClassLibrary;

public class Grid
{
	private readonly int _bombs;
	private readonly Random rnd = new();
	private Tile[,] _gameGrid;
	public int Columns { get; private set; }
	public int Rows { get; private set; }
	public Tile this[int x, int y]
	{
		get => _gameGrid[x, y];
		set => _gameGrid[x, y] = value;

	}

	public Grid(int rows, int columns, int bombs)
	{
		Columns = columns;
		Rows = rows;
		_bombs = rows * columns / 2 > bombs ? bombs : rows * columns / 2;
		InitializeGameGrid();
	}
	private bool IsInBoundCheck(int x, int y)
	{
		return x >= 0 && y >= 0 && x < Rows && y < Columns;
	}
	private void InitializeGameGrid()
	{
		_gameGrid = new Tile[Rows, Columns];

		for (int r = 0; r < Rows; r++)
		{
			for (int c = 0; c < Columns; c++)
			{
				_gameGrid[r, c] = new Tile(r, c);
			}
		}
	}
	public void SetBombs()
	{
		int bombCount = _bombs;
		while (bombCount > 0)
		{
			var row = rnd.Next(0, Rows);
			var col = rnd.Next(0, Columns);
			if (!_gameGrid[row, col].IsVisited)
			{
				_gameGrid[row, col].State = 9;
				bombCount--;
			}

		}
	}

	public void SetTileState(Tile tile)
	{
		if (tile.IsBomb()) return;

		int bombCount = 0;

		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				if (IsInBoundCheck(tile.X + i, tile.Y + j))
				{
					if (_gameGrid[tile.X + i, tile.Y + j].IsBomb()) bombCount++;
				}

			}
		}

		tile.State = bombCount;
	}

	public void SetAllTilesState()
	{
		foreach (var tile in _gameGrid)
		{
			SetTileState(tile);
		}
	}

	public IEnumerable<Tile> GetTilesAround(Tile tile)
	{
		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				if(IsInBoundCheck(tile.X + i, tile.Y + j))
				yield return _gameGrid[tile.X + i, tile.Y + j];

			}
		}
	}
	public void RevealTiles(IEnumerable<Tile> tiles)
	{
		foreach(var tile in tiles) tile.IsRevealed= true;
	}
	public void VisitAround(Tile tile)
	{
		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				if (IsInBoundCheck(tile.X + i, tile.Y + j))
				{
					//if (!_gameGrid[tile.X + i, tile.Y + j].IsBomb() &&
					//	!_gameGrid[tile.X + i, tile.Y + j].IsRevealed)

					_gameGrid[tile.X + i, tile.Y + j].IsVisited = true;



				}

			}
		}
	}
	public void UnVisitAround(Tile tile)
	{
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (IsInBoundCheck(tile.X + i, tile.Y + j))
                {   

                    _gameGrid[tile.X + i, tile.Y + j].IsVisited = false;

                }

            }
        }
    }
	public void RevealAll()
	{
		foreach (var tile in _gameGrid)
		{
			tile.IsRevealed = true;
		}
	}
	public void FloodFill(Tile tile)
	{
		var que = new Queue<Tile>(GetTilesAround(tile));
		RevealTiles(que);
		tile.IsVisited= true;

		while (que.Count > 0)
		{
			var processedTile = que.Dequeue();           
            if (processedTile.State == 0 && !processedTile.IsVisited)
			{
                processedTile.IsVisited = true;
                var tilesAround = GetTilesAround(processedTile);
				RevealTiles(tilesAround);
				foreach (var cell in tilesAround)
				{
					if (cell.State == 0 && !cell.IsVisited) que.Enqueue(cell);
				}
			}
		}

	}
}