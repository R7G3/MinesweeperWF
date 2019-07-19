using System.Collections.Generic;

namespace MinesweeperWF.Minesweeper
{
    class Board : IBoard
    {
        private IBoard Strategy { get; set; }
        public Cell[,] board;

        internal Board(IBoard Strategy)
        {
            this.Strategy = Strategy;
            board = new Cell[Settings.X, Settings.Y];
        }

        public HashSet<Cell> GetNeighboringCells(Cell cell, Cell[,] board)
        {
            return Strategy.GetNeighboringCells(cell, board);
        }

        public void Fill(Cell[,] board)
        {
            Strategy.Fill(board);
        }
    }
}
