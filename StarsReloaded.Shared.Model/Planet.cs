namespace StarsReloaded.Shared.Model
{
    public class Planet
    {
        public Planet(int x, int y, string name)
        {
            X = x;
            Y = y;
            Name = name;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public string Name { get; set; }
    }
}
