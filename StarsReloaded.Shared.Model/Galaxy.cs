namespace StarsReloaded.Shared.Model
{
    using System.Collections.Generic;

    public class Galaxy
    {
        public Galaxy()
        {
            Planets = new List<Planet>();
        }

        public IList<Planet> Planets { get; private set; }
    }
}
