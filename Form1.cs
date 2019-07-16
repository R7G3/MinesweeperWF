using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinesweeperWF.Minesweeper;
using MinesweeperWF.Minesweeper.Boards;

namespace MinesweeperWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        internal Board mineField;

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            if (Settings.Type == Settings.TypeOfBoard.Square)
            {
                flowLayoutPanel1.Controls.Clear();
                int btnSize = 25;
                flowLayoutPanel1.Size = new Size(Settings.X * btnSize + 4, Settings.Y * btnSize + 4);
                //Board squareBoard = new Board(new Square());
                mineField = new Board(new Square());
                mineField.Fill(Settings.Y, Settings.X, mineField.board, Settings.CountOfBombs);
                flowLayoutPanel1.Controls.AddRange(mineField.CellsboardToControls(mineField.board)); //Print
            }
            //Form1_Load_1(sender = new object(), e = new EventArgs());
        }
    }
}
