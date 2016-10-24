namespace StarsReloaded.Client.ViewModel.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    using GalaSoft.MvvmLight.Messaging;

    using StarsReloaded.Client.ViewModel.Attributes;
    using StarsReloaded.Client.ViewModel.Fragments;
    using StarsReloaded.Client.ViewModel.Messages;
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

        private readonly IPlanetSimulationService planetSimulationService;
        private PlanetWrapper selectedPlanet;
        private HabitationBarControlViewModel gravityBar;
        private HabitationBarControlViewModel temperatureBar;
        private HabitationBarControlViewModel radiationBar;

        private double mineralChartWidth;

        #endregion

        #region Constructors

        public SummaryPanelViewModel(IPlanetSimulationService planetSimulationService)
        {
            this.planetSimulationService = planetSimulationService;

            Messenger.Default.Register<GameStateLoadedMessage>(this, this.OnGameStateLoaded);
            Messenger.Default.Register<PlanetSelectedMessage>(this, this.OnPlanetSelected);

            this.GravityBar = ViewModelLocator.HabitationBarControl;
            this.GravityBar.ParameterType = HabitationParameterType.Gravity;

            this.TemperatureBar = ViewModelLocator.HabitationBarControl;
            this.TemperatureBar.ParameterType = HabitationParameterType.Temperature;

            this.RadiationBar = ViewModelLocator.HabitationBarControl;
            this.RadiationBar.ParameterType = HabitationParameterType.Radiation;

            if (this.IsInDesignMode)
            {
                this.GravityBar.Range = HabitationRange.Immunity;
                this.TemperatureBar.Range = new HabitationRange(-35, +5);
                this.RadiationBar.Range = new HabitationRange(-20, +30);

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
                                SurfaceIronium = 750,
                                SurfaceBoranium = 2700,
                                SurfaceGermanium = 1250
                            });

                this.MineralChartWidth = 350;
            }
        }

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
                this.UpdatePlanetValues();
            }
        }

        [DependsUpon(nameof(SelectedPlanet))]
        public HabitationBarControlViewModel GravityBar
        {
            get
            {
                return this.gravityBar;
            }

            set
            {
                this.Set(() => this.GravityBar, ref this.gravityBar, value);
            }
        }

        [DependsUpon(nameof(SelectedPlanet))]
        public HabitationBarControlViewModel TemperatureBar
        {
            get
            {
                return this.temperatureBar;
            }

            set
            {
                this.Set(() => this.TemperatureBar, ref this.temperatureBar, value);
            }
        }

        [DependsUpon(nameof(SelectedPlanet))]
        public HabitationBarControlViewModel RadiationBar
        {
            get
            {
                return this.radiationBar;
            }

            set
            {
                this.Set(() => this.RadiationBar, ref this.radiationBar, value);
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
                  * this.planetSimulationService.GetMiningRate(this.SelectedPlanet.Model, MineralType.Ironium)
                  / MaxSurfaceMinerals;

        [DependsUpon(nameof(MineralChartWidth))]
        [DependsUpon(nameof(SelectedPlanet))]
        public double MinedBoraniumBarPos
            =>
            this.SelectedPlanet == null
                ? 0
                : this.MineralChartWidth
                  * this.planetSimulationService.GetMiningRate(this.SelectedPlanet.Model, MineralType.Boranium)
                  / MaxSurfaceMinerals;

        [DependsUpon(nameof(MineralChartWidth))]
        [DependsUpon(nameof(SelectedPlanet))]
        public double MinedGermaniumBarPos
            =>
            this.SelectedPlanet == null
                ? 0
                : this.MineralChartWidth
                  * this.planetSimulationService.GetMiningRate(this.SelectedPlanet.Model, MineralType.Germanium)
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

        #region Private methods

        private void OnGameStateLoaded(GameStateLoadedMessage message)
        {
            this.UpdateHabRanges(message.GameState.CurrentPlayerRace);
        }

        private void OnPlanetSelected(PlanetSelectedMessage message)
        {
            this.SelectedPlanet = message.Planet;
        }

        private void UpdateHabRanges(PlayerRace race)
        {
            this.GravityBar.Range = race.GravityTolerance;
            this.TemperatureBar.Range = race.TemperatureTolerance;
            this.RadiationBar.Range = race.RadiationTolerance;

            this.GravityBar.MaxTerraformTech = race.GetMaxTerraformTech(HabitationParameterType.Gravity);
            this.TemperatureBar.MaxTerraformTech = race.GetMaxTerraformTech(HabitationParameterType.Temperature);
            this.RadiationBar.MaxTerraformTech = race.GetMaxTerraformTech(HabitationParameterType.Radiation);
        }

        private void UpdatePlanetValues()
        {
            this.GravityBar.CurrentValue = this.SelectedPlanet?.Gravity.Model;
            this.TemperatureBar.CurrentValue = this.SelectedPlanet?.Temperature.Model;
            this.RadiationBar.CurrentValue = this.SelectedPlanet?.Radiation.Model;

            this.GravityBar.OriginalValue = this.SelectedPlanet?.OriginalGravity.Model;
            this.TemperatureBar.OriginalValue = this.SelectedPlanet?.OriginalTemperature.Model;
            this.RadiationBar.OriginalValue = this.SelectedPlanet?.OriginalRadiation.Model;
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