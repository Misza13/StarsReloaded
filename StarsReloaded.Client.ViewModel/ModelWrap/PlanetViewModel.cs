namespace StarsReloaded.Client.ViewModel.ModelWrap
{
    using GalaSoft.MvvmLight.CommandWpf;
    using StarsReloaded.Shared.Model;

    public class PlanetViewModel
    {
        public RelayCommand<PlanetViewModel> SelectPlanetCommand { get; set; }

        private readonly Planet _planet;

        private const int Radius = 1;

        public PlanetViewModel(Planet planet, RelayCommand<PlanetViewModel> selectPlanetCommand)
        {
            SelectPlanetCommand = selectPlanetCommand;
            _planet = planet;
        }

        public int X => this._planet.X;

        public int Y => this._planet.Y;

        public string Name => this._planet.Name;

        public int Left => this._planet.X - Radius;

        public int Right => this._planet.Y - Radius;
    }
}
