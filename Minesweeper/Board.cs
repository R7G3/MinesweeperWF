using System.Collections.Generic;

namespace MinesweeperWF.Minesweeper
{
    class Board : IBoard
    {
        private IBoard Strategy { get; set; }
        public Cell[,] board;

        internal Board(IBoard Strategy)
        {
            this.Strategy = Strategy; //example: Strategy = new Square();
            board = new Cell[Settings.X, Settings.Y];
        }

        public List<Cell> GetNeighboringCells(Cell cell, Cell[,] board)
        {
            return Strategy.GetNeighboringCells(cell, board);
        }

        public void Fill(int Y, int X, Cell[,] board, int countOfBombs)
        {
            Strategy.Fill(Y, X, board, countOfBombs);
        }
    }
}
