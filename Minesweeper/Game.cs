using System.Collections.Generic;
using MinesweeperWF.Minesweeper;
using MinesweeperWF.Minesweeper.Boards;

namespace MinesweeperWF
{
    internal class Game
    {
        internal Board mineField;
        internal bool isGameOver;
        internal bool isWin;

        internal Game()
        {
            isGameOver = false;
            isWin = false;
            if (Settings.BoardType == Settings.TypeOfBoard.Square)
            {
                mineField = new Board(new Square());
                mineField.Fill(mineField.board);
            }
        }

        internal void Open(int coordX, int coordY)
        {
            Cell clickedCell = mineField.board[coordX, coordY];
            State cellState = clickedCell.GetState();
            Value cellValue = clickedCell.GetValue();

            if (cellValue == Value.Bomb)
            {
                isGameOver = true;
                clickedCell.SetState(State.Opened);
            }
            else if (cellValue == Value.Number)
            {
                if (cellState == State.Closed)
                {
                    clickedCell.SetState(State.Opened);
                }
                else if (cellState == State.Opened)
                {
                    ClickedOnNumberBesideFlags(clickedCell, mineField.board);
                }
            }
            else if (cellValue == Value.Empty) //TODO: почему-то открывает не все
            {
                clickedCell.SetState(State.Opened);
                List<Cell> neighbouringCells = mineField.GetNeighboringCells(clickedCell, mineField.board);
                foreach (Cell cell in neighbouringCells)
                {
                    if (cell.GetValue() != Value.Bomb)
                    {
                        cell.SetState(State.Opened);
                    }
                }
            }
        }

        private void ClickedOnNumberBesideFlags(Cell clickedCell, Cell[,] board)
        {
            List<Cell> neighbouringCells = mineField.GetNeighboringCells(clickedCell, mineField.board);
            List<Cell> flaggedCells = new List<Cell>();
            foreach (Cell cell in neighbouringCells)
            {
                if (cell.GetState() == State.Flagged)
                {
                    flaggedCells.Add(cell);
                }
            }
            if (flaggedCells.Count == clickedCell.CountOfNeighboringBombs)
            {
                foreach (Cell cell in neighbouringCells)
                {
                    if (cell.GetState() != State.Flagged)
                    {
                        cell.SetState(State.Opened);
                        if (cell.GetValue() == Value.Bomb)
                        {
                            isGameOver = true;
                        }
                    }
                }
            }
        }

        internal void SetFlag(int x, int y)
        {
            Cell cell = mineField.board[x, y];
            if (cell.GetState() == State.Flagged)
            {
                cell.SetState(State.Closed);
            }
            else if (cell.GetState() != State.Opened)
            {
                cell.SetState(State.Flagged);
            }
        }

        internal bool CheckWin()
        {
            Cell[,] board = mineField.board;
            int size = board.GetLength(0) * board.GetLength(1);
            int closed = 0;
            for (int rows = 0; rows < board.GetLength(0); rows++)
            {
                for (int cols = 0; cols < board.GetLength(1); cols++)
                {
                    if (board[cols, rows].GetState() != State.Opened)
                    {
                        closed++;
                    }
                }
            }
            return closed == Settings.CountOfBombs ? true : false;
        }
    }
}
