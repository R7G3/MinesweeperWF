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

            switch (cellValue)
            {
                case Value.Bomb:
                    isGameOver = true;
                    clickedCell.SetState(State.Opened);
                    break;

                case Value.Number:
                    if (cellState == State.Closed)
                    {
                        clickedCell.SetState(State.Opened);
                    }
                    else if (cellState == State.Opened)
                    {
                        ClickedOnNumberBesideFlags(clickedCell, mineField.board);
                    }
                    break;

                case Value.Empty:
                    clickedCell.SetState(State.Opened);
                    OpenEmptyArea(clickedCell);
                    break;
            }
        }
        
        private void ClickedOnNumberBesideFlags(Cell clickedCell, Cell[,] board)
        {
            HashSet<Cell> neighbouringCells = mineField.GetNeighboringCells(clickedCell, mineField.board);
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

        private void OpenEmptyArea(Cell clickedCell)
        {//
            HashSet<Cell> whatsNeedOpen = new HashSet<Cell>();
            HashSet<Cell> firstGen = new HashSet<Cell>();
            HashSet<Cell> secondGen = new HashSet<Cell>();
            firstGen = mineField.GetNeighboringCells(clickedCell, mineField.board);

            bool isHaveMoreNeighbours = false;
            do
            {
                isHaveMoreNeighbours = false;
                List<Cell> notFiltered = new List<Cell>();
                foreach (Cell c in firstGen)
                {
                    notFiltered.AddRange(mineField.GetNeighboringCells(c, mineField.board));
                }

                List<Cell> filtered = new List<Cell>();
                foreach (Cell c in notFiltered)
                {
                    if (c.GetState() == State.Closed && (c.GetValue() == Value.Empty || c.GetValue() == Value.Number))
                    {
                        if (c.GetValue() == Value.Number)
                        {
                            c.SetState(State.Opened);
                        }
                        else
                        {
                            filtered.Add(c);
                        }
                    }
                }

                foreach (Cell f in filtered)
                {
                    if (!whatsNeedOpen.Contains(f))
                    {
                        secondGen.Add(f);
                        isHaveMoreNeighbours = true;
                    }
                }

                foreach (Cell c in firstGen)
                {
                    whatsNeedOpen.Add(c);
                }

                firstGen.Clear();

                foreach (Cell c in secondGen)
                {
                    firstGen.Add(c);
                }
            } while (isHaveMoreNeighbours);

            foreach (Cell c in whatsNeedOpen)
            {
                c.SetState(State.Opened);
            }
        }
    }
}
