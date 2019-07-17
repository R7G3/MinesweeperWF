using System.Windows.Forms;

namespace MinesweeperWF
{
    internal partial class GameButton : Button
    {
        internal int coordX;
        internal int coordY;

        internal GameButton(int col, int row)
        {
            coordX = col;
            coordY = row;
        }
    }
}
