namespace StarsReloaded.Shared.Model
{
    public class Planet
    {
        public Planet(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }

        public int Y { get; private set; }
    }
}
