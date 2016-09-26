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
        private PlanetViewModel _selectedPlanet;

        public ObservableCollection<PlanetViewModel> Planets { get; set; }

        public MapPanelControlViewModel()
        {
            var selectPlanetCommand = new RelayCommand<PlanetViewModel>(this.SelectPlanet);

            var worldGen = new GalaxyGenerator(600, 600);
            var galaxy = worldGen.GenerateUniform(100);
            this.Planets = new ObservableCollection<PlanetViewModel>(
                galaxy.Planets.Select(p => new PlanetViewModel(p, selectPlanetCommand)));
            this._selectedPlanet = this.Planets[0];
        }

        public MapPanelControlViewModel(Galaxy galaxy)
        {
            var selectPlanetCommand = new RelayCommand<PlanetViewModel>(this.SelectPlanet);

            this.Planets = new ObservableCollection<PlanetViewModel>(
                galaxy.Planets.Select(p => new PlanetViewModel(p, selectPlanetCommand)));
        }

        public string SelectedObjectName => _selectedPlanet == null ? string.Empty : _selectedPlanet.Name;

        public string SelectedObjectCoords => _selectedPlanet == null ? string.Empty : $"[{_selectedPlanet.X},{_selectedPlanet.Y}]";

        private void SelectPlanet(PlanetViewModel planet)
        {
            this._selectedPlanet = planet;
            RaisePropertyChanged(nameof(SelectedObjectName));
            RaisePropertyChanged(nameof(SelectedObjectCoords));
        }
    }
}
