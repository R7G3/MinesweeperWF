using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperWF.Minesweeper
{
    interface IBoard
    {
        List<Cell> GetNeighboringCells(Cell cell, Cell[,] board);
        void Fill(int y, int x, Cell[,] board, int countOfBombs);
    }
}
