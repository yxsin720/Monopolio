namespace Monopolio.Models
{
    public class Player
    {
        public string Name { get; set; }

        public int Money { get; set; }
        public int NumGames { get; set; }
        public int NumWins { get; set; }
        public int NumDraws { get; set; }
        public int NumLosses { get; set; }

        public int Row { get; set; }
        public int Col { get; set; }

        public Player(string name)
        {
            Name = name;
            Money = 0;
            NumGames = 0;
            NumWins = 0;
            NumDraws = 0;
            NumLosses = 0;
            Row = 0;
            Col = 0;
        }

        public override string ToString()
        {
            return $"{Name} {NumGames} {NumWins} {NumDraws} {NumLosses}";
        }
    }
}
