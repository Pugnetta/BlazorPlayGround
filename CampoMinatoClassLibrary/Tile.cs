using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampoMinatoClassLibrary;

public class Tile
{
	public int X { get; private set; }
	public int Y { get; private set; }
	public int State { get; set; }
	public bool IsFlagged { get; set; }
	public bool IsRevealed { get; set; }
	public bool IsVisited { get; set; }
	public Tile(int x, int y)
	{
		X = x;
		Y = y;
	}
	public bool IsBomb() => State == 9;
	public void Flag()
	{
		if (!IsRevealed)
		{
			IsFlagged = !IsFlagged;
		}
	}
	public void Revealed()
	{
		IsRevealed = true;
		IsFlagged= false;
	}
}
