using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinesweeperWF.Minesweeper;

namespace MinesweeperWF
{
    public partial class GameButton : Button
    {
        public int coordX;
        public int coordY;

        public GameButton(int col, int row)
        {
            coordX = col;
            coordY = row;
        }
    }
}
