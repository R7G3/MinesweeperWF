namespace MinesweeperWF.Minesweeper
{
    internal static class Settings
    {
        internal enum TypeOfBoard
        {
            Square
        }

        internal static int X = 10;
        internal static int Y = 10;
        internal static int CountOfBombs = 15;
        internal static int ButtonSize = 25;
        internal static TypeOfBoard BoardType = TypeOfBoard.Square;
    }
}
