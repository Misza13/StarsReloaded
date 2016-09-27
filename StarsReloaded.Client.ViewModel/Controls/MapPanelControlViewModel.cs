namespace StarsReloaded.Client.ViewModel.Controls
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using StarsReloaded.Client.ViewModel.ModelWrap;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen;

    public class MapPanelControlViewModel : ViewModelBase
    {
        private readonly Galaxy _galaxy;
        private PlanetViewModel _selectedPlanet;

        public ObservableCollection<PlanetViewModel> Planets { get; set; }

        public MapPanelControlViewModel()
        {
            var selectPlanetCommand = new RelayCommand<PlanetViewModel>(this.SelectPlanet);

            var worldGen = new GalaxyGenerator(800, 800);
            this._galaxy = worldGen.GenerateUniform(100);
            this.Planets = new ObservableCollection<PlanetViewModel>(
                this._galaxy.Planets.Select(p => new PlanetViewModel(p, selectPlanetCommand)));
            this._selectedPlanet = this.Planets[0];
        }

        public MapPanelControlViewModel(Galaxy galaxy)
        {
            _galaxy = galaxy;
            var selectPlanetCommand = new RelayCommand<PlanetViewModel>(this.SelectPlanet);

            this.Planets = new ObservableCollection<PlanetViewModel>(
                galaxy.Planets.Select(p => new PlanetViewModel(p, selectPlanetCommand)));
        }

        public string SelectedObjectName => _selectedPlanet == null ? string.Empty : _selectedPlanet.Name;

        public string SelectedObjectCoords => _selectedPlanet == null ? string.Empty : $"[{_selectedPlanet.X},{_selectedPlanet.Y}]";

        public int GalaxyWidth => this._galaxy.Width;

        public int GalaxyHeight => this._galaxy.Height;

        private void SelectPlanet(PlanetViewModel planet)
        {
            this._selectedPlanet = planet;
            RaisePropertyChanged(nameof(SelectedObjectName));
            RaisePropertyChanged(nameof(SelectedObjectCoords));
        }
    }
}
