using System.Windows.Forms;

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
