namespace StarsReloaded.Client.ViewModel.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using StarsReloaded.Client.ViewModel.ModelWrap;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    public class MapPanelControlViewModel : ViewModelBase
    {
        private Galaxy galaxy;
        private PlanetViewModel selectedPlanet;

        public MapPanelControlViewModel(IGalaxyGeneratorService galaxyGeneratorService)
        {
            if (this.IsInDesignMode)
            {
                this.Galaxy = galaxyGeneratorService.Generate(GalaxySize.Medium, GalaxyDensity.Dense, PlanetDistribution.UniformClumping);
            }

            this.MapClickCommand = new RelayCommand<Point>(this.MapClick);
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

        public RelayCommand<Point> MapClickCommand { get; }

        private void Initialize(Galaxy fromGalaxy)
        {
            this.galaxy = fromGalaxy;

            this.Planets = new ObservableCollection<PlanetViewModel>(
                this.galaxy.Planets.Select(p => new PlanetViewModel(p)));
        }

        private void SelectPlanet(PlanetViewModel planet)
        {
            this.selectedPlanet = planet;
            this.RaisePropertyChanged(nameof(this.SelectedObjectName));
            this.RaisePropertyChanged(nameof(this.SelectedObjectCoords));
        }

        private void MapClick(Point p)
        {
            var closest = this.Planets.Aggregate(
                (curClosest, pl) =>
                    {
                        if (curClosest == null)
                        {
                            return pl;
                        }

                        if (Math.Pow(p.X - pl.X, 2) + Math.Pow(p.Y - pl.Y, 2) < Math.Pow(p.X - curClosest.X, 2) + Math.Pow(p.Y - curClosest.Y, 2))
                        {
                            return pl;
                        }

                        return curClosest;
                    });

            this.SelectPlanet(closest);
        }
    }
}
