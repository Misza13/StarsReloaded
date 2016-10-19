namespace StarsReloaded.Client.ViewModel.ModelWrap
{
    using StarsReloaded.Shared.Model;

    public class PlanetViewModel : BaseViewModel
    {
        #region Private fields

        private const int Radius = 1;
        private readonly Planet planet;

        #endregion

        #region Constructors

        public PlanetViewModel(Planet planet)
        {
            this.planet = planet;
        }

        #endregion

        #region Public properties

        public int X => this.planet.X;

        public int Y => this.planet.Y;

        public string Name => this.planet.Name;

        public int Left => this.planet.X - Radius;

        public int Right => this.planet.Y - Radius;

        #endregion
    }
}
