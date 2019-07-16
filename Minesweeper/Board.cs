using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperWF.Minesweeper
{
    class Board : IBoard
    {
        private IBoard BoardType { get; set; }
        //public static Coordinates Size;
        public Cell[,] board = new Cell[10, 10];// = new Cell[Size.Y, Size.X];
        //public int countOfBombs;

        void SetSize() { }
        void GetSize() { }

        internal Board(IBoard boardType)
        {
            BoardType = boardType; //example: Strategy = new Square();
        }

        public List<Cell> GetNeighboringCells(Cell cell, Cell[,] board)
        {
            return BoardType.GetNeighboringCells(cell, board);
        }

        public void OpenCells(List<Cell> cells)
        {
            BoardType.OpenCells(cells);
        }

        public void Fill(int Y, int X, Cell[,] board, int countOfBombs)
        {
            BoardType.Fill(Y, X, board, countOfBombs); //TODO: , int countOfBombs
        }

        public Control[] CellsboardToControls(Cell[,] board)
        {
            return BoardType.CellsboardToControls(board);
        }
    }
}
