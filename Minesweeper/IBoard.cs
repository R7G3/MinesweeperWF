using System.Collections.Generic;

namespace MinesweeperWF.Minesweeper
{
    interface IBoard
    {
        List<Cell> GetNeighboringCells(Cell cell, Cell[,] board);
        void Fill(int y, int x, Cell[,] board, int countOfBombs);
    }
}
