using System;
using System.Collections.Generic;

namespace MinesweeperWF.Minesweeper.Boards
{
    class Square : IBoard
    {
        public void Fill(Cell[,] board)
        {
            for (int cols = 0; cols < Settings.Y; cols++)
            {
                for (int rows = 0; rows < Settings.X; rows++)
                {
                    board[cols, rows] = new Cell(rows, cols);
                    board[cols, rows].SetValue(Value.Empty);
                    board[cols, rows].SetState(State.Closed);
                }
            }
            PlaceBombs(board, Settings.CountOfBombs);
        }

        private void PlaceBombs(Cell[,] board, int countOfBombs)
        {
            int i = 0;
            List<Cell> cellsNeighboringBombs = new List<Cell>();
            while (i < countOfBombs)
            {
                Random rand = new Random();
                int y = rand.Next(0, board.GetLength(0) - 1);
                int x = rand.Next(0, board.GetLength(1) - 1);
                if (board[y, x].GetValue() != Value.Bomb)
                {
                    board[y, x].SetValue(Value.Bomb);
                    board[y, x].SetState(State.Closed);
                    i++;
                    HashSet<Cell> cells = GetNeighboringCells(board[y, x], board);
                    cellsNeighboringBombs.AddRange(cells);
                }
            }
            foreach (Cell cell in cellsNeighboringBombs)
            {
                if (cell.GetValue() != Value.Bomb)
                {
                    cell.SetValue(Value.Number);
                    cell.CountOfNeighboringBombs++;
                }
            }
        }

        private bool isValidCoordinates(Cell[,] board, int biasY, int biasX)
        {
            if (biasY >= 0 && biasY < board.GetLength(1))
            {
                if (biasX >= 0 && biasX < board.GetLength(0))
                {
                    return true;
                }
            }
            return false;
        }

        public HashSet<Cell> GetNeighboringCells(Cell clickedCell, Cell[,] board)
        {
            HashSet<Cell> NeighboringCells = new HashSet<Cell>();
            int clickedY = clickedCell.Y;
            int clickedX = clickedCell.X;

            for (int y = clickedY - 1; y <= clickedY + 1; y++) 
            {
                for (int x = clickedX - 1; x <= clickedX + 1; x++)
                {
                    if (isValidCoordinates(board, y, x))
                    {
                        if ((x != clickedX) || (y != clickedY))
                        {
                            NeighboringCells.Add(board[y, x]);
                        }
                    }
                }
            }

            return NeighboringCells;
        }
    }
}
