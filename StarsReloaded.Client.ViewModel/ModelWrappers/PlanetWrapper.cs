namespace StarsReloaded.Client.ViewModel.ModelWrappers
{
    using StarsReloaded.Shared.Model;

    public class PlanetWrapper : BaseWrapper<Planet>
    {
        private const int Radius = 1;

        public PlanetWrapper(Planet planet) : base(planet)
        {
            this.Gravity = new GravityWrapper(planet.Gravity);
            this.Temperature = new TemperatureWrapper(planet.Temperature);
            this.Radiation = new RadiationWrapper(planet.Radiation);
        }

        public int X => this.Model.X;

        public int Y => this.Model.Y;

        public string Name => this.Model.Name;

        public int Left => this.Model.X - Radius;

        public int Right => this.Model.Y - Radius;

        public GravityWrapper Gravity { get; private set; }

        public TemperatureWrapper Temperature { get; private set; }

        public RadiationWrapper Radiation { get; private set; }
    }
}
