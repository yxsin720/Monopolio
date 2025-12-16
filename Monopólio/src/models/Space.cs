namespace Monopolio.Models
{
    public class Space
    {
        public string Name { get; set; }
        public string? Owner { get; set; }
        public int Houses { get; set; }

        public Space(string name)
        {
            Name = name;
            Owner = null;
            Houses = 0;
        }
    }
}
