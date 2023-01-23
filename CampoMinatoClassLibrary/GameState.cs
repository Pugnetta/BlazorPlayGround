using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampoMinatoClassLibrary
{
	public class GameState
	{

		public int Rows { get; private set; } = 10;
		public int Cols { get; private set; } = 10;
		public int Bombs { get; private set; } = 20;
		public bool GameOver { get; private set; }
		public GameStatus Status { get; set; } = GameStatus.AwaitingFristMove;

		
		
		public Grid GameGrid { get; set; }

		public GameState()
		{
			GameGrid = new(Rows, Cols, Bombs);

		}

		public void MakeMove(Tile clickedTile)
		{
			if (clickedTile.State == 0) GameGrid.FloodFill(clickedTile);
			else if (clickedTile.IsBomb()) GameOver = true;
			else clickedTile.IsRevealed = true;
		}
		public void PutFlag(Tile clickedTile)
		{
			clickedTile.IsFlagged = !clickedTile.IsFlagged;
		}

	}
}
