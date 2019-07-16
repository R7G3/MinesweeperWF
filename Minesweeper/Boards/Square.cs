using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperWF.Minesweeper.Boards
{
    class Square : IBoard
    {
        public void OpenCells(List<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                cell.Open();
            }
        }

        //public void ViewBoardOnForm(Cell[,] board)
        //{
        //    //
        //}

        public Control[] CellsboardToControls(Cell[,] board)
        {
            Control[] cells = new Control[board.GetLength(0) * board.GetLength(1)]; //[]{};
            int index = 0;
            while (index < cells.Length)
            {
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    for (int y = 0; y < board.GetLength(1); y++)
                    {
                        cells[index] = board[x, y].btn;
                        //cells[i] = board[x, y].btn;
                        index++;
                    }//Index out of array exception
                }
            }
            return cells;
        }

        public void Fill(int y, int x, Cell[,] board, int countOfBombs)
        {
            for (int cols = 0; cols < y; cols++)
            {
                for (int rows = 0; rows < x; rows++)
                {
                    //board[cols, rows] = new Cell(cols, rows);
                    board[cols, rows] = new Cell(rows, cols);
                    board[cols, rows].SetValue(Value.Empty);
                }
            }
            PlaceBombs(board, countOfBombs);
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
                if (board[y, x].value != Value.Bomb)
                {
                    board[y, x].SetValue(Value.Bomb);
                    board[y, x].SetState(State.Closed);
                    i++;
                    List<Cell> cells = GetNeighboringCells(board[y, x], board);
                    cellsNeighboringBombs.AddRange(cells);
                }
            }
            foreach (Cell cell in cellsNeighboringBombs)
            {
                if (cell.value != Value.Bomb)
                {
                    cell.SetValue(Value.Number);
                    cell.CountOfNeighboringBombs++;
                    //cell.btn.Text = cell.CountOfNeighboringBombs.ToString();
                }
            }
        }

        private bool isNeighboring(Cell[,] board, int biasY, int biasX)
        {
            if (biasY >= 0 && biasY < board.GetLength(1)) //0 is Y or X, if arr[Y,X]?
            {
                if (biasX >= 0 && biasX < board.GetLength(0))
                {
                    return true;
                    /*if (isEmptyOrHaveNumber(board[biasY, biasX], board))
                    {
                        return true;
                    }*/
                }
            }
            return false;
        }

        private bool isEmptyOrHaveNumber(Cell cell, Cell[,] board)
        {
            int Y = cell.Y;
            int X = cell.X;
            //if (board[Y, X].GetState() == State.Empty || board[Y, X].GetState() == State.Number)
            if (board[Y, X].value == Value.Bomb)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        public List<Cell> GetNeighboringCells(Cell clickedCell, Cell[,] board)
        {
            int Y = clickedCell.Y;
            int X = clickedCell.X;
            List<Cell> NeighboringCells = new List<Cell>();

            int radius = 1;
            int biasX; //bias - смещённый
            int biasY;

            //то что дальше вынести в функцию, повторять с radius++ пока не вернётся пустой список

            //Left
            biasX = X - radius;
            biasY = Y;
            if (isNeighboring(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            //Left-Up
            biasX = X - radius;
            biasY = Y - radius;
            if (isNeighboring(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            //Up
            biasX = X;
            biasY = Y - radius;
            if (isNeighboring(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            //Right-Up
            biasX = X + radius;
            biasY = Y - radius;
            if (isNeighboring(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            //Right
            biasX = X + radius;
            biasY = Y;
            if (isNeighboring(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            //Right-Down
            biasX = X + radius;
            biasY = Y + radius;
            if (isNeighboring(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            //Down
            biasX = X;
            biasY = Y + radius;
            if (isNeighboring(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            //Left-Down
            biasX = X - radius;
            biasY = Y + radius;
            if (isNeighboring(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            return NeighboringCells;
        }
    }
}
