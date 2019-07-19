using System.Collections.Generic;

namespace MinesweeperWF.Minesweeper
{
    interface IBoard
    {//
        HashSet<Cell> GetNeighboringCells(Cell cell, Cell[,] board);
        void Fill(Cell[,] board);
    }
}
