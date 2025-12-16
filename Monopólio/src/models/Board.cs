using System;

namespace Monopolio.Models
{
    public class Board
    {
        public const int Size = 7;
        private Space[,] grid = new Space[Size, Size];

        public void Set(int row, int col, Space space)
        {
            grid[row, col] = space;
        }

        public Space Get(int row, int col)
        {
            return grid[row, col];
        }

        public (int row, int col) GetStart()
        {
            return (Size / 2, Size / 2);
        }

        public (int row, int col) Wrap(int row, int col)
        {
            if (row < 0)
                row = Size - 1;

            if (row >= Size)
                row = 0;

            if (col < 0)
                col = Size - 1;

            if (col >= Size)
                col = 0;

            return (row, col);
        }

        public int GetPrice(string spaceName)
        {
            if (spaceName == "Brown1") return 100;
            if (spaceName == "Brown2") return 120;

            if (spaceName == "Teal1") return 90;
            if (spaceName == "Teal2") return 130;

            if (spaceName == "Orange1") return 120;
            if (spaceName == "Orange2") return 120;
            if (spaceName == "Orange3") return 140;

            if (spaceName == "Black1") return 110;
            if (spaceName == "Black2") return 120;
            if (spaceName == "Black3") return 130;

            if (spaceName == "Red1") return 130;
            if (spaceName == "Red2") return 130;
            if (spaceName == "Red3") return 160;

            if (spaceName == "Green1") return 120;
            if (spaceName == "Green2") return 140;
            if (spaceName == "Green3") return 160;

            if (spaceName == "Blue1") return 140;
            if (spaceName == "Blue2") return 140;
            if (spaceName == "Blue3") return 170;

            if (spaceName == "Pink1") return 160;
            if (spaceName == "Pink2") return 180;

            if (spaceName == "White1") return 160;
            if (spaceName == "White2") return 180;
            if (spaceName == "White3") return 190;

            if (spaceName == "Yellow1") return 140;
            if (spaceName == "Yellow2") return 140;
            if (spaceName == "Yellow3") return 170;

            if (spaceName == "Violet1") return 150;
            if (spaceName == "Violet2") return 130;

            if (spaceName == "Train1") return 150;
            if (spaceName == "Train2") return 150;
            if (spaceName == "Train3") return 150;
            if (spaceName == "Train4") return 150;

            if (spaceName == "ElectricCompany") return 120;
            if (spaceName == "WaterWorks") return 120;

            return -1;
        }
    }
}
