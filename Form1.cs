using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinesweeperWF;
using MinesweeperWF.Minesweeper;
using MinesweeperWF.Minesweeper.Boards;

namespace MinesweeperWF
{
    internal partial class Form1 : Form
    {
        internal Form1()
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
        
        private GameButton[] buttons = new GameButton[Settings.X * Settings.Y];
        private Game game;

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            game = new Game();
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.Size = new Size(Settings.X * Settings.ButtonSize + 4, Settings.Y * Settings.ButtonSize + 4);
            CreateButtons();
            flowLayoutPanel1.Controls.AddRange(buttons.Cast<Control>().ToArray());
        }

        private void UpdateField(Cell[,] board)
        {
            for (int coordY = 0; coordY < board.GetLength(0); coordY++)
            {
                for (int coordX = 0; coordX < board.GetLength(1); coordX++)
                {
                    int index = coordY * Settings.Y + coordX;
                    UpdateButton(buttons[index], board[coordX, coordY]);
                }
            }
        }

        private void UpdateButton(Button button, Cell cell)
        {
            switch (cell.GetState())
            {
                case State.Flagged:
                    button.Text = "F";
                    button.ForeColor = Color.Red;
                    button.Font = new Font(button.Font, FontStyle.Bold);
                    break;

                case State.Closed:
                    button.Text = " ";
                    break;

                case State.Opened:
                    switch (cell.GetValue())
                    {
                        case Value.Empty:
                            button.Text = " ";
                            button.FlatStyle = FlatStyle.Flat;
                            button.FlatAppearance.BorderSize = 0;
                            button.Enabled = false;
                            break;

                        case Value.Number:
                            button.Text = cell.CountOfNeighboringBombs.ToString();
                            button.FlatStyle = FlatStyle.Flat;
                            break;

                        case Value.Bomb:
                            button.Text = "☼";
                            button.Font = new Font(button.Font, FontStyle.Bold);
                            button.BackColor = Color.Red;
                            break;
                    }
                    break;
            }
        }

        private void GameButtonClick(object sender, MouseEventArgs e)
        {
            int coordX = ((GameButton)sender).coordX;
            int coordY = ((GameButton)sender).coordY;
            if (e.Button == MouseButtons.Left)
            {
                game.Open(coordX, coordY);
                UpdateField(game.mineField.board);
                WinLooseVerification();
            }
            else if (e.Button == MouseButtons.Right)
            {
                game.SetFlag(coordX, coordY);
                int index = coordY * Settings.Y + coordX;
                Cell cell = game.mineField.board[coordX, coordY];
                UpdateButton(buttons[index], cell);
            }
            else if (e.Button == MouseButtons.Middle)
            {
                Cell cell = game.mineField.board[coordX, coordY];
                MessageBox.Show(
                    "DEBUG!\n\nState: \t\t"  + cell.GetState().ToString()
                    + "\nValue: \t\t"        + cell.GetValue().ToString()
                    + "\nNeighb. bombs: \t"  + cell.CountOfNeighboringBombs.ToString()
                    + "\nCoords: \t\t"       + cell.X + ", " + cell.Y
                );
            }
        }

        private void BlockGameButtons()
        {
            foreach (Button b in buttons)
            {
                b.Enabled = false;
            }
        }

        private void WinLooseVerification()
        {
            bool endGame = game.CheckWin() || game.isGameOver;
            if (endGame)
            {
                BlockGameButtons();
                string message = game.isGameOver ? "You loose!" : "You win!";
                MessageBox.Show(message);
            }
        }

        private void CreateButtons()
        {
            int index = 0;
            int countOfButtons = Settings.X * Settings.Y;
            for (int row = 0; row < Settings.X; row++)
            {
                for (int col = 0; col < Settings.Y; col++)
                {
                    GameButton btn = new GameButton(col, row);
                    btn.Width = btn.Height = Settings.ButtonSize;
                    btn.Margin = new Padding(0);
                    btn.MouseDown += new MouseEventHandler(this.GameButtonClick);
                    buttons[index] = btn;
                    index++;
                }
            }
        }
    }
}
