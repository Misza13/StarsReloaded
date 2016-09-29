namespace StarsReloaded.Client.ViewModel.ModelWrap
{
    using GalaSoft.MvvmLight.CommandWpf;
    using StarsReloaded.Shared.Model;

    public class PlanetViewModel
    {
        private const int Radius = 1;

        private readonly Planet planet;

        public PlanetViewModel(Planet planet, RelayCommand<PlanetViewModel> selectPlanetCommand)
        {
            this.SelectPlanetCommand = selectPlanetCommand;
            this.planet = planet;
        }

        public RelayCommand<PlanetViewModel> SelectPlanetCommand { get; set; }

        public int X => this.planet.X;

        public int Y => this.planet.Y;

        public string Name => this.planet.Name;

        public int Left => this.planet.X - Radius;

        public int Right => this.planet.Y - Radius;
    }
}
