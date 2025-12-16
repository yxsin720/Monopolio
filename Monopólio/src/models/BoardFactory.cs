namespace Monopolio.Models
{
    public static class BoardFactory
    {
        public static Board Create()
        {
            var b = new Board();

            b.Set(0, 0, new Space("Prison"));
            b.Set(0, 1, new Space("Green3"));
            b.Set(0, 2, new Space("Violet1"));
            b.Set(0, 3, new Space("Train2"));
            b.Set(0, 4, new Space("Red3"));
            b.Set(0, 5, new Space("White1"));
            b.Set(0, 6, new Space("BackToStart"));

            b.Set(1, 0, new Space("Blue3"));
            b.Set(1, 1, new Space("Community"));
            b.Set(1, 2, new Space("Red2"));
            b.Set(1, 3, new Space("Violet2"));
            b.Set(1, 4, new Space("WaterWorks"));
            b.Set(1, 5, new Space("Chance"));
            b.Set(1, 6, new Space("White2"));

            b.Set(2, 0, new Space("Blue2"));
            b.Set(2, 1, new Space("Red1"));
            b.Set(2, 2, new Space("Chance"));
            b.Set(2, 3, new Space("Brown2"));
            b.Set(2, 4, new Space("Community"));
            b.Set(2, 5, new Space("Black1"));
            b.Set(2, 6, new Space("LuxTax"));

            b.Set(3, 0, new Space("Train1"));
            b.Set(3, 1, new Space("Green2"));
            b.Set(3, 2, new Space("Teal1"));
            b.Set(3, 3, new Space("Start"));
            b.Set(3, 4, new Space("Teal2"));
            b.Set(3, 5, new Space("Black2"));
            b.Set(3, 6, new Space("Train3"));

            b.Set(4, 0, new Space("Blue1"));
            b.Set(4, 1, new Space("Green1"));
            b.Set(4, 2, new Space("Community"));
            b.Set(4, 3, new Space("Brown1"));
            b.Set(4, 4, new Space("Chance"));
            b.Set(4, 5, new Space("Black3"));
            b.Set(4, 6, new Space("White3"));

            b.Set(5, 0, new Space("Pink1"));
            b.Set(5, 1, new Space("Chance"));
            b.Set(5, 2, new Space("Orange1"));
            b.Set(5, 3, new Space("Orange2"));
            b.Set(5, 4, new Space("Orange3"));
            b.Set(5, 5, new Space("Community"));
            b.Set(5, 6, new Space("Yellow3"));

            b.Set(6, 0, new Space("FreePark"));
            b.Set(6, 1, new Space("Pink2"));
            b.Set(6, 2, new Space("ElectricCompany"));
            b.Set(6, 3, new Space("Train4"));
            b.Set(6, 4, new Space("Yellow1"));
            b.Set(6, 5, new Space("Yellow2"));
            b.Set(6, 6, new Space("Police"));

            return b;
        }
    }
}
