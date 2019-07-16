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
        //private Coordinates Position;
        public int X;
        public int Y;
        public State state;
        public Value value;
        public int CountOfNeighboringBombs = 0;
        public Button btn;

        public void Open()
        {
            if (state != State.Flagged)
            {
                if (value == Value.Bomb)
                {
                    //TODO: сделать gameover
                }
                else
                {
                    //нужно открыть клетку и все соседние включая номера
                    //как вызвать GetNeighboringCells?
                }
            }
            //TODO: сделать проверку, flaggedCells == CountOfBombs?
        }

        public void SetState(State state)
        {
            this.state = state;
            if (state == State.Closed)
            {
                btn.FlatStyle = FlatStyle.Standard;
            }
            else if (state == State.Opened)
            {
                btn.FlatStyle = FlatStyle.Flat;
            }
        }

        public State GetState()
        {
            return state;
        }

        public void SetValue(Value value) //TODO: этот метод вообще нужен?
        {
            this.value = value;
            if (value == Value.Bomb)
            {
                //btn.Text = "☼";//>|<
            }
            else if (value == Value.Number)
            {
                //Number = Board.GetCountOfBombsInNeighbouringCells();
            }
            else if (value == Value.Empty)
            {
                //btn.Text = String.Empty;
            }
        }

        public Value GetValue()
        {
            return value;
        }

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
            btn = new Button();
            btn.Width = btn.Height = Settings.ButtonSize;
            btn.Margin = new Padding(0);
            /*btn.Click += Click;
            btn.DoubleClick += DoubleClick;*/

            btn.MouseDown += new MouseEventHandler(this.MouseDown);
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                //LeftMouseButton();
                //Open();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (state == State.Flagged)
                {
                    SetState(State.Closed);
                }
                else
                {
                    SetState(State.Flagged);
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                MessageBox.Show(
                    "DEBUG!\n\nState: \t\t" + state.ToString()
                    + "\nValue: \t\t" + value.ToString()
                    + "\nNeighb. bombs: \t" + CountOfNeighboringBombs.ToString()
                    + "\nCoords: \t\t" + X + ", " + Y
                );
            }
        }

        /*public void Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Clicked!");
            this.btn.FlatStyle = FlatStyle.Flat;
            this.btn.FlatAppearance.BorderSize = 0;
            /*if (e.Button == MouseButtons.Left)
            {
                MessageBox.Show("Left");
            }
            if (e.Button == MouseButtons.Right)
            {
                MessageBox.Show("Right");
            }
        }*/

        /*public void DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("DoubleClick");
            this.btn.Text = "F";
            this.SetState(State.Flagged);
        }*/
    }
}
