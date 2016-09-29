namespace StarsReloaded.Client.ViewModel.Controls
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using StarsReloaded.Client.ViewModel.ModelWrap;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen.Services;

    public class MapPanelControlViewModel : ViewModelBase
    {
        private Galaxy galaxy;
        private PlanetViewModel selectedPlanet;

        public MapPanelControlViewModel(IGalaxyGeneratorService galaxyGeneratorService)
        {
            if (this.IsInDesignMode)
            {
                this.Galaxy = galaxyGeneratorService.GenerateUniform(800, 800, 100);
            }
        }

        public ObservableCollection<PlanetViewModel> Planets { get; set; }

        public Galaxy Galaxy
        {
            set
            {
                if (value != this.galaxy)
                {
                    this.Initialize(value);
                    this.RaisePropertyChanged(nameof(this.Planets));
                    this.RaisePropertyChanged(nameof(this.GalaxyWidth));
                    this.RaisePropertyChanged(nameof(this.GalaxyHeight));
                    this.SelectPlanet(null);
                }
            }
        }

        public string SelectedObjectName => this.selectedPlanet == null ? string.Empty : this.selectedPlanet.Name;

        public string SelectedObjectCoords => this.selectedPlanet == null ? string.Empty : $"[{this.selectedPlanet.X},{this.selectedPlanet.Y}]";

        public int GalaxyWidth => this.galaxy.Width;

        public int GalaxyHeight => this.galaxy.Height;

        private void Initialize(Galaxy fromGalaxy)
        {
            this.galaxy = fromGalaxy;

            var selectPlanetCommand = new RelayCommand<PlanetViewModel>(this.SelectPlanet);

            this.Planets = new ObservableCollection<PlanetViewModel>(
                this.galaxy.Planets.Select(p => new PlanetViewModel(p, selectPlanetCommand)));
        }

        private void SelectPlanet(PlanetViewModel planet)
        {
            this.selectedPlanet = planet;
            this.RaisePropertyChanged(nameof(this.SelectedObjectName));
            this.RaisePropertyChanged(nameof(this.SelectedObjectCoords));
        }
    }
}
