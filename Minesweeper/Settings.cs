using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperWF.Minesweeper
{
    public static class Settings
    {
        public enum TypeOfBoard
        {
            Square
        }

        public static int X = 10;
        public static int Y = 10;
        public static int CountOfBombs = 15;
        public static int ButtonSize = 25;
        public static TypeOfBoard BoardType = TypeOfBoard.Square;
    }
}
