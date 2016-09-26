namespace StarsReloaded.Shared.Model
{
    using System.Collections.Generic;

    public class Galaxy
    {
        public Galaxy()
        {
            Planets = new List<Planet>();
        }

        public List<Planet> Planets { get; private set; }
    }
}
