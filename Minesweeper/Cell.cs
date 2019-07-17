namespace MinesweeperWF
{
    internal enum State
    {
        Opened,
        Closed,
        Flagged
    }

    internal enum Value
    {
        Bomb,
        Number,
        Empty
    }

    internal class Cell
    {
        internal int X;
        internal int Y;
        private State state;
        private Value value;
        internal int CountOfNeighboringBombs = 0;

        internal void SetState(State state)
        {
            this.state = state;
        }

        internal State GetState()
        {
            return state;
        }

        internal void SetValue(Value value)
        {
            this.value = value;
        }

        internal Value GetValue()
        {
            return value;
        }

        internal Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
