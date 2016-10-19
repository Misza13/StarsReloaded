namespace StarsReloaded.Client.ViewModel.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    using GalaSoft.MvvmLight.CommandWpf;
    using GalaSoft.MvvmLight.Messaging;

    using StarsReloaded.Client.ViewModel.Attributes;
    using StarsReloaded.Client.ViewModel.Messages;
    using StarsReloaded.Client.ViewModel.ModelWrap;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    public class MapPanelControlViewModel : BaseViewModel
    {
        #region Private fields

        private Galaxy galaxy;
        private PlanetViewModel selectedPlanet;

        #endregion

        #region Constructors

        public MapPanelControlViewModel(IGalaxyGeneratorService galaxyGeneratorService)
        {
            if (this.IsInDesignMode)
            {
                this.Galaxy = galaxyGeneratorService.Generate(GalaxySize.Medium, GalaxyDensity.Dense, PlanetDistribution.UniformClumping);
            }

            this.MapClickCommand = new RelayCommand<Point>(this.MapClick);
        }

        #endregion

        #region Public properties

        [DependsUpon(nameof(Galaxy))]
        public ObservableCollection<PlanetViewModel> Planets { get; set; }

        public Galaxy Galaxy
        {
            set
            {
                if (value != this.galaxy)
                {
                    this.Initialize(value);
                    this.RaisePropertyChanged(nameof(this.Galaxy));
                    this.SelectPlanet(null);
                }
            }
        }

        public string SelectedObjectName => this.selectedPlanet == null ? string.Empty : this.selectedPlanet.Name;

        public string SelectedObjectCoords => this.selectedPlanet == null ? string.Empty : $"[{this.selectedPlanet.X},{this.selectedPlanet.Y}]";

        [DependsUpon(nameof(Galaxy))]
        public int GalaxyWidth => this.galaxy.Width;

        [DependsUpon(nameof(Galaxy))]
        public int GalaxyHeight => this.galaxy.Height;

        #endregion

        #region Commands

        public RelayCommand<Point> MapClickCommand { get; }

        #endregion

        #region Private methods
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

            Messenger.Default.Send(new PlanetSelectedMessage(planet));
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

        #endregion
    }
}
