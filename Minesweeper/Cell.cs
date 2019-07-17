using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinesweeperWF.Minesweeper;

namespace MinesweeperWF
{
    public enum State
    {
        Opened,
        Closed,
        Flagged
    }

    public enum Value
    {
        Bomb,
        Number,
        Empty
    }

    class Cell
    {
        public int X;
        public int Y;
        public State state;
        public Value value;
        public int CountOfNeighboringBombs = 0;

        public void SetState(State state)
        {
            this.state = state;
        }

        public State GetState()
        {
            return state;
        }

        public void SetValue(Value value)
        {
            this.value = value;
        }

        public Value GetValue()
        {
            return value;
        }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
