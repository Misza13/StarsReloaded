namespace StarsReloaded.Shared.Model
{
    using System.Collections.Generic;

    public class Galaxy
    {
        public Galaxy(int width, int height)
        {
            Width = width;
            Height = height;
            Planets = new List<Planet>();
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public List<Planet> Planets { get; private set; }
    }
}
