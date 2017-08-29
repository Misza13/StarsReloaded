namespace StarsReloaded.Client.ViewModel.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using StarsReloaded.Client.Mediation.Messages;
    using StarsReloaded.Client.ViewModel.Attributes;
    using StarsReloaded.Client.ViewModel.ModelWrappers;
    using StarsReloaded.Shared.Guts.Services;
    using StarsReloaded.Shared.Model;

    public class SummaryPanelViewModel : BaseViewModel
    {
        #region Private constants

        private const int MaxSurfaceMinerals = 5000;
        private const int MaxMineralConcentration = 120;

        #endregion

        #region Private fields

        private double mineralChartWidth;

        private PlanetWrapper selectedPlanet;

        private HabitationRange gravityRange;
        private HabitationRange temperatureRange;
        private HabitationRange radiationRange;

        private int maxGravityTerraform;
        private int maxTemperatureTerraform;
        private int maxRadiationTerraform;

        #endregion

        #region Constructors

        public SummaryPanelViewModel()
        {
            this.MineralChartResizedCommand = new RelayCommand<double>(this.OnMineralChartResized);

            Messenger.Default.Register<GameStateLoadedMessage>(this, this.OnGameStateLoaded);
            Messenger.Default.Register<PlanetSelectedMessage>(this, this.OnPlanetSelected);

            if (this.IsInDesignMode)
            {
                this.GravityRange = HabitationRange.Immunity;
                this.TemperatureRange = new HabitationRange(-35, +5);
                this.RadiationRange = new HabitationRange(-20, +30);

                this.SelectedPlanet =
                    new PlanetWrapper(
                        new Planet(13, 37)
                            {
                                Name = Guid.NewGuid().ToString(),
                                Gravity = new HabitationParameter(27),
                                Temperature = new HabitationParameter(46),
                                Radiation = new HabitationParameter(25),
                                OriginalGravity = new HabitationParameter(35),
                                OriginalTemperature = new HabitationParameter(12),
                                OriginalRadiation = new HabitationParameter(0),
                                IroniumConcentration = new MineralConcentration(35),
                                BoraniumConcentration = new MineralConcentration(55),
                                GermaniumConcentration = new MineralConcentration(15),
                                SurfaceIronium = 750,
                                SurfaceBoranium = 2700,
                                SurfaceGermanium = 1250
                            });

                this.MineralChartWidth = 350;
            }
        }

        #endregion

        #region Dependencies

        public IPlanetSimulationService PlanetSimulationService { get; set; }

        #endregion

        #region Public properties

        [DependsUpon(nameof(SelectedPlanet))]
        public Visibility PlanetSummaryVisibility => this.SelectedPlanet != null ? Visibility.Visible : Visibility.Hidden;

        [DependsUpon(nameof(SelectedPlanet))]
        public string PlanetName => this.SelectedPlanet?.Name ?? string.Empty;

        [DependsUpon(nameof(SelectedPlanet))]
        public string GravityDisplayValue => this.SelectedPlanet?.Gravity.DisplayValue ?? string.Empty;

        [DependsUpon(nameof(SelectedPlanet))]
        public string TemperatureDisplayValue => this.SelectedPlanet?.Temperature.DisplayValue ?? string.Empty;

        [DependsUpon(nameof(SelectedPlanet))]
        public string RadiationDisplayValue => this.SelectedPlanet?.Radiation.DisplayValue ?? string.Empty;

        public PlanetWrapper SelectedPlanet
        {
            get
            {
                return this.selectedPlanet;
            }

            private set
            {
                this.Set(() => this.SelectedPlanet, ref this.selectedPlanet, value);
            }
        }

        public HabitationRange GravityRange
        {
            get
            {
                return this.gravityRange;
            }

            set
            {
                this.Set(() => this.GravityRange, ref this.gravityRange, value);
            }
        }

        public HabitationRange TemperatureRange
        {
            get
            {
                return this.temperatureRange;
            }

            set
            {
                this.Set(() => this.TemperatureRange, ref this.temperatureRange, value);
            }
        }

        public HabitationRange RadiationRange
        {
            get
            {
                return this.radiationRange;
            }

            set
            {
                this.Set(() => this.RadiationRange, ref this.radiationRange, value);
            }
        }

        public int MaxGravityTerraform
        {
            get
            {
                return this.maxGravityTerraform;
            }

            set
            {
                this.Set(() => this.MaxGravityTerraform, ref this.maxGravityTerraform, value);
            }
        }

        public int MaxTemperatureTerraform
        {
            get
            {
                return this.maxTemperatureTerraform;
            }

            set
            {
                this.Set(() => this.MaxTemperatureTerraform, ref this.maxTemperatureTerraform, value);
            }
        }

        public int MaxRadiationTerraform
        {
            get
            {
                return this.maxRadiationTerraform;
            }

            set
            {
                this.Set(() => this.MaxRadiationTerraform, ref this.maxRadiationTerraform, value);
            }
        }

        public double MineralChartWidth
        {
            get
            {
                return this.mineralChartWidth;
            }

            set
            {
                this.Set(() => this.MineralChartWidth, ref this.mineralChartWidth, value);
            }
        }

        [DependsUpon(nameof(MineralChartWidth))]
        public ObservableCollection<ChartLineElement> ChartLines => this.GetChartLines();

        [DependsUpon(nameof(SelectedPlanet))]
        public int GravityCurrentValue => this.SelectedPlanet?.Model.Gravity.Clicks ?? 0;

        [DependsUpon(nameof(SelectedPlanet))]
        public int TemperatureCurrentValue => this.SelectedPlanet?.Model.Temperature.Clicks ?? 0;

        [DependsUpon(nameof(SelectedPlanet))]
        public int RadiationCurrentValue => this.SelectedPlanet?.Model.Radiation.Clicks ?? 0;

        [DependsUpon(nameof(SelectedPlanet))]
        public int GravityOriginalValue => this.SelectedPlanet?.Model.OriginalGravity.Clicks ?? 0;

        [DependsUpon(nameof(SelectedPlanet))]
        public int TemperatureOriginalValue => this.SelectedPlanet?.Model.OriginalTemperature.Clicks ?? 0;

        [DependsUpon(nameof(SelectedPlanet))]
        public int RadiationOriginalValue => this.SelectedPlanet?.Model.OriginalRadiation.Clicks ?? 0;

        [DependsUpon(nameof(MineralChartWidth))]
        [DependsUpon(nameof(SelectedPlanet))]
        public double IroniumBarPos
            => this.MineralChartWidth * this.SelectedPlanet?.Model.SurfaceIronium / MaxSurfaceMinerals ?? 0;

        [DependsUpon(nameof(MineralChartWidth))]
        [DependsUpon(nameof(SelectedPlanet))]
        public double BoraniumBarPos
            => this.MineralChartWidth * this.SelectedPlanet?.Model.SurfaceBoranium / MaxSurfaceMinerals ?? 0;

        [DependsUpon(nameof(MineralChartWidth))]
        [DependsUpon(nameof(SelectedPlanet))]
        public double GermaniumBarPos
            => this.MineralChartWidth * this.SelectedPlanet?.Model.SurfaceGermanium / MaxSurfaceMinerals ?? 0;

        [DependsUpon(nameof(MineralChartWidth))]
        [DependsUpon(nameof(SelectedPlanet))]
        public double MinedIroniumBarPos
            =>
            this.SelectedPlanet == null
                ? 0
                : this.MineralChartWidth
                  * this.PlanetSimulationService.GetMiningRate(this.SelectedPlanet.Model, MineralType.Ironium)
                  / MaxSurfaceMinerals;

        [DependsUpon(nameof(MineralChartWidth))]
        [DependsUpon(nameof(SelectedPlanet))]
        public double MinedBoraniumBarPos
            =>
            this.SelectedPlanet == null
                ? 0
                : this.MineralChartWidth
                  * this.PlanetSimulationService.GetMiningRate(this.SelectedPlanet.Model, MineralType.Boranium)
                  / MaxSurfaceMinerals;

        [DependsUpon(nameof(MineralChartWidth))]
        [DependsUpon(nameof(SelectedPlanet))]
        public double MinedGermaniumBarPos
            =>
            this.SelectedPlanet == null
                ? 0
                : this.MineralChartWidth
                  * this.PlanetSimulationService.GetMiningRate(this.SelectedPlanet.Model, MineralType.Germanium)
                  / MaxSurfaceMinerals;

        [DependsUpon(nameof(MineralChartWidth))]
        [DependsUpon(nameof(SelectedPlanet))]
        public double IroniumConcPos
            =>
            this.MineralChartWidth * this.SelectedPlanet?.Model.IroniumConcentration.Concentration
            / MaxMineralConcentration ?? 0;

        [DependsUpon(nameof(MineralChartWidth))]
        [DependsUpon(nameof(SelectedPlanet))]
        public double BoraniumConcPos
            =>
            this.MineralChartWidth * this.SelectedPlanet?.Model.BoraniumConcentration.Concentration
            / MaxMineralConcentration ?? 0;

        [DependsUpon(nameof(MineralChartWidth))]
        [DependsUpon(nameof(SelectedPlanet))]
        public double GermaniumConcPos
            =>
            this.MineralChartWidth * this.SelectedPlanet?.Model.GermaniumConcentration.Concentration
            / MaxMineralConcentration ?? 0;

        #endregion

        #region Commands

        public RelayCommand<double> MineralChartResizedCommand { get; private set; }

        #endregion

        #region Private methods

        private void OnGameStateLoaded(GameStateLoadedMessage message)
        {
            this.UpdateHabRanges(message.GameState.CurrentPlayerRace);
        }

        private void OnPlanetSelected(PlanetSelectedMessage message)
        {
            this.SelectedPlanet = message.Planet != null ? new PlanetWrapper(message.Planet) : null;
        }

        private void OnMineralChartResized(double newWidth)
        {
            this.MineralChartWidth = newWidth;
            Console.WriteLine("resized:" + newWidth);
        }

        private void UpdateHabRanges(PlayerRace race)
        {
            this.GravityRange = race.GravityTolerance;
            this.TemperatureRange = race.TemperatureTolerance;
            this.RadiationRange = race.RadiationTolerance;

            this.MaxGravityTerraform = race.GetMaxTerraformTech(HabitationParameterType.Gravity);
            this.MaxTemperatureTerraform = race.GetMaxTerraformTech(HabitationParameterType.Temperature);
            this.MaxRadiationTerraform = race.GetMaxTerraformTech(HabitationParameterType.Radiation);
        }

        private ObservableCollection<ChartLineElement> GetChartLines()
        {
            return
                new ObservableCollection<ChartLineElement>(
                    Enumerable.Range(0, 11).Select(i => new ChartLineElement(i * 500, 5000, this.MineralChartWidth)));
        }

        #endregion

        #region Public classes

        public class ChartLineElement
        {
            private int max;
            private double width;

            public ChartLineElement(int mark, int max, double width)
            {
                this.Mark = mark;
                this.max = max;
                this.width = width;
            }

            public int Mark { get; }

            public double Position => this.width * this.Mark / this.max;

            public double LabelPosition => this.Position - 12;
        }

        #endregion
    }
}