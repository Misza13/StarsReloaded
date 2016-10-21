namespace StarsReloaded.Client.ViewModel.ModelWrappers
{
    using StarsReloaded.Shared.Model;

    public class PlanetWrapper : BaseViewModel
    {
        #region Private fields

        private const int Radius = 1;
        private readonly Planet planet;

        #endregion

        #region Constructors

        public PlanetWrapper(Planet planet)
        {
            this.planet = planet;
            this.Gravity = new GravityWrapper(planet.Gravity);
            this.Temperature = new TemperatureWrapper(planet.Temperature);
            this.Radiation = new RadiationWrapper(planet.Radiation);
        }

        #endregion

        #region Public properties

        public int X => this.planet.X;

        public int Y => this.planet.Y;

        public string Name => this.planet.Name;

        public int Left => this.planet.X - Radius;

        public int Right => this.planet.Y - Radius;

        public GravityWrapper Gravity { get; private set; }

        public TemperatureWrapper Temperature { get; private set; }

        public RadiationWrapper Radiation { get; private set; }

        #endregion
    }
}
