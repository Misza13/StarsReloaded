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
        private Galaxy _galaxy;
        private PlanetViewModel _selectedPlanet;

        public ObservableCollection<PlanetViewModel> Planets { get; set; }

        public MapPanelControlViewModel()
        {
            if (IsInDesignMode)
            {
                var worldGen = new GalaxyGenerator(800, 800);
                this.Galaxy = worldGen.GenerateUniform(100);
            }
        }

        public Galaxy Galaxy
        {
            set
            {
                if (value != _galaxy)
                {
                    Initialize(value);
                    RaisePropertyChanged(nameof(Planets));
                    RaisePropertyChanged(nameof(GalaxyWidth));
                    RaisePropertyChanged(nameof(GalaxyHeight));
                    SelectPlanet(null);
                }
            }
        }

        public string SelectedObjectName => _selectedPlanet == null ? string.Empty : _selectedPlanet.Name;

        public string SelectedObjectCoords => _selectedPlanet == null ? string.Empty : $"[{_selectedPlanet.X},{_selectedPlanet.Y}]";

        public int GalaxyWidth => this._galaxy.Width;

        public int GalaxyHeight => this._galaxy.Height;

        private void Initialize(Galaxy galaxy)
        {
            this._galaxy = galaxy;

            var selectPlanetCommand = new RelayCommand<PlanetViewModel>(this.SelectPlanet);

            this.Planets = new ObservableCollection<PlanetViewModel>(
                this._galaxy.Planets.Select(p => new PlanetViewModel(p, selectPlanetCommand)));
        }

        private void SelectPlanet(PlanetViewModel planet)
        {
            this._selectedPlanet = planet;
            RaisePropertyChanged(nameof(SelectedObjectName));
            RaisePropertyChanged(nameof(SelectedObjectCoords));
        }
    }
}
