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
                this.MapClick(new Point() { X = 50, Y = 50 });
            }

            this.MapClickCommand = new RelayCommand<Point>(this.MapClick);
        }

        #endregion

        #region Public properties

        [DependsUpon(nameof(Galaxy))]
        public ObservableCollection<PlanetViewModel> Planets { get; set; }

        public Galaxy Galaxy
        {
            private get
            {
                return this.galaxy;
            }

            set
            {
                if (value != this.galaxy)
                {
                    this.Initialize(value);
                    this.SelectedPlanet = null;
                    this.RaisePropertyChanged();
                }
            }
        }

        public PlanetViewModel SelectedPlanet
        {
            private get
            {
                return this.selectedPlanet;
            }

            set
            {
                if (value != this.selectedPlanet)
                {
                    this.selectedPlanet = value;
                    this.RaisePropertyChanged();

                    Messenger.Default.Send(new PlanetSelectedMessage(value));
                }
            }
        }

        [DependsUpon(nameof(SelectedPlanet))]
        public string SelectedObjectName => this.SelectedPlanet == null ? string.Empty : this.SelectedPlanet.Name;

        [DependsUpon(nameof(SelectedPlanet))]
        public string SelectedObjectCoords => this.SelectedPlanet == null ? string.Empty : $"[{this.SelectedPlanet.X},{this.SelectedPlanet.Y}]";

        [DependsUpon(nameof(SelectedPlanet))]
        public int SelectedObjectX => this.SelectedPlanet?.X ?? 0;

        [DependsUpon(nameof(SelectedPlanet))]
        public int SelectedObjectY => this.SelectedPlanet?.Y ?? 0;

        [DependsUpon(nameof(SelectedPlanet))]
        public Visibility SelectionArrowVisibility => this.SelectedPlanet != null ? Visibility.Visible : Visibility.Hidden;

        [DependsUpon(nameof(Galaxy))]
        public int GalaxyWidth => this.Galaxy?.Width ?? 0;

        [DependsUpon(nameof(Galaxy))]
        public int GalaxyHeight => this.Galaxy?.Height ?? 0;

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

            this.SelectedPlanet = closest;
        }

        #endregion
    }
}
