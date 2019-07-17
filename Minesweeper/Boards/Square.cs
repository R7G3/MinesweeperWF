﻿using System;
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
                    List<Cell> cells = GetNeighboringCells(board[y, x], board);
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

        public List<Cell> GetNeighboringCells(Cell clickedCell, Cell[,] board)
        {
            int Y = clickedCell.Y;
            int X = clickedCell.X;
            List<Cell> NeighboringCells = new List<Cell>();

            int radius = 1;
            int biasX; //bias - смещённый
            int biasY;

            //TODO: подумать, можно ли как-то это упростить:
            //Left
            biasX = X - radius;
            biasY = Y;
            if (isValidCoordinates(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            //Left-Up
            biasX = X - radius;
            biasY = Y - radius;
            if (isValidCoordinates(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            //Up
            biasX = X;
            biasY = Y - radius;
            if (isValidCoordinates(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            //Right-Up
            biasX = X + radius;
            biasY = Y - radius;
            if (isValidCoordinates(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            //Right
            biasX = X + radius;
            biasY = Y;
            if (isValidCoordinates(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            //Right-Down
            biasX = X + radius;
            biasY = Y + radius;
            if (isValidCoordinates(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            //Down
            biasX = X;
            biasY = Y + radius;
            if (isValidCoordinates(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            //Left-Down
            biasX = X - radius;
            biasY = Y + radius;
            if (isValidCoordinates(board, biasY, biasX))
            {
                NeighboringCells.Add(board[biasY, biasX]);
            }

            return NeighboringCells;
        }
    }
}
