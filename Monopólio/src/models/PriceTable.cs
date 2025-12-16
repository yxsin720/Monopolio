using System.Collections.Generic;

namespace Monopolio.Models
{
    public static class PriceTable
    {
        private static Dictionary<string, int> prices = new Dictionary<string, int>()
        {
            { "Brown1", 100 }, { "Brown2", 120 },
            { "Teal1", 90 }, { "Teal2", 130 },
            { "Orange1", 120 }, { "Orange2", 120 }, { "Orange3", 140 },
            { "Black1", 110 }, { "Black2", 120 }, { "Black3", 130 },
            { "Red1", 130 }, { "Red2", 130 }, { "Red3", 160 },
            { "Green1", 120 }, { "Green2", 140 }, { "Green3", 160 },
            { "Blue1", 140 }, { "Blue2", 140 }, { "Blue3", 170 },
            { "Pink1", 160 }, { "Pink2", 180 },
            { "White1", 160 }, { "White2", 180 }, { "White3", 190 },
            { "Yellow1", 140 }, { "Yellow2", 140 }, { "Yellow3", 170 },
            { "Violet1", 150 }, { "Violet2", 130 },
            { "Train1", 150 }, { "Train2", 150 },
            { "Train3", 150 }, { "Train4", 150 },
            { "ElectricCompany", 120 },
            { "WaterWorks", 120 }
        };

        public static int GetPrice(string spaceName)
        {
            if (prices.ContainsKey(spaceName))
                return prices[spaceName];

            return -1;
        }

        private static Dictionary<string, string> colors = new Dictionary<string, string>()
        {
            { "Brown1", "Brown" }, { "Brown2", "Brown" },
            { "Teal1", "Teal" }, { "Teal2", "Teal" },
            { "Orange1", "Orange" }, { "Orange2", "Orange" }, { "Orange3", "Orange" },
            { "Black1", "Black" }, { "Black2", "Black" }, { "Black3", "Black" },
            { "Red1", "Red" }, { "Red2", "Red" }, { "Red3", "Red" },
            { "Green1", "Green" }, { "Green2", "Green" }, { "Green3", "Green" },
            { "Blue1", "Blue" }, { "Blue2", "Blue" }, { "Blue3", "Blue" },
            { "Pink1", "Pink" }, { "Pink2", "Pink" },
            { "White1", "White" }, { "White2", "White" }, { "White3", "White" },
            { "Yellow1", "Yellow" }, { "Yellow2", "Yellow" }, { "Yellow3", "Yellow" },
            { "Violet1", "Violet" }, { "Violet2", "Violet" }
        };

        public static string? GetColor(string spaceName)
        {
            if (colors.ContainsKey(spaceName))
                return colors[spaceName];

            return null;
        }
    }
}
