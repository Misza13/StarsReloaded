namespace StarsReloaded.Client.ViewModel.Windows
{
    using StarsReloaded.Client.ViewModel.Attributes;
    using StarsReloaded.Client.ViewModel.Controls;
    using StarsReloaded.Client.ViewModel.Fragments;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    public sealed class MainWindowViewModel : BaseViewModel
    {
        #region Private fields

        private Galaxy galaxy;
        private Race playerRace;

        #endregion

        #region Constructors

        public MainWindowViewModel(IGalaxyGeneratorService galaxyGeneratorService)
        {
            if (this.IsInDesignMode)
            {
                var galaxy = galaxyGeneratorService.Generate(GalaxySize.Medium, GalaxyDensity.Packed, PlanetDistribution.UniformClumping);

                var race = new Race()
                    {
                        GravityTolerance = new HabitationRange(-30, +20),
                        TemperatureTolerance = new HabitationRange(-50, +10),
                        RadiationTolerance = new HabitationRange(-20, +25)
                    };

                this.Initialize(galaxy, race);
            }
        }

        #endregion

        #region Public properties

        public Galaxy Galaxy => this.galaxy;

        public Race PlayerRace => this.playerRace;

        [DependsUpon(nameof(Galaxy))]
        public MapPanelControlViewModel MapPanelControlViewModel { get; private set; }
        
        [DependsUpon(nameof(PlayerRace))]
        public SummaryPanelControlViewModel SummaryPanelControlViewModel { get; private set; }

        #endregion

        #region Public methods

        public void Initialize(Galaxy newGalaxy, Race newPlayerRace)
        {
            this.galaxy = newGalaxy;
            this.playerRace = newPlayerRace;

            this.MapPanelControlViewModel = ViewModelLocator.MapPanelControl;
            this.MapPanelControlViewModel.Galaxy = this.galaxy;
            this.RaisePropertyChanged(nameof(this.MapPanelControlViewModel));

            this.SummaryPanelControlViewModel = ViewModelLocator.SummaryPanelControl;
            this.SummaryPanelControlViewModel.GravityBarViewModel = new HabitationBarControlViewModel()
                {
                    ParameterType = HabitationParameterType.Gravity,
                    Range = this.playerRace.GravityTolerance
                };
            this.SummaryPanelControlViewModel.TemperatureBarViewModel = new HabitationBarControlViewModel()
                {
                    ParameterType = HabitationParameterType.Temperature,
                    Range = this.playerRace.TemperatureTolerance
                };
            this.SummaryPanelControlViewModel.RadiationBarViewModel = new HabitationBarControlViewModel()
                {
                    ParameterType = HabitationParameterType.Radiation,
                    Range = this.playerRace.RadiationTolerance
                };
            this.RaisePropertyChanged(nameof(this.SummaryPanelControlViewModel));
        }

        #endregion
    }
}