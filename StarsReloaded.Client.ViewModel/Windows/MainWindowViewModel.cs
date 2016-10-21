namespace StarsReloaded.Client.ViewModel.Windows
{
    using GalaSoft.MvvmLight.Messaging;

    using StarsReloaded.Client.ViewModel.Attributes;
    using StarsReloaded.Client.ViewModel.Controls;
    using StarsReloaded.Client.ViewModel.Fragments;
    using StarsReloaded.Client.ViewModel.Messages;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    public sealed class MainWindowViewModel : BaseViewModel
    {
        #region Private fields

        private MapPanelControlViewModel mapPanelControlViewModel;
        private SummaryPanelControlViewModel summaryPanelControlViewModel;

        #endregion

        #region Constructors

        public MainWindowViewModel(IGalaxyGeneratorService galaxyGeneratorService)
        {
            if (this.IsInDesignMode)
            {
                var galaxy = galaxyGeneratorService.Generate(GalaxySize.Medium, GalaxyDensity.Packed, PlanetDistribution.UniformClumping);

                var race = new PlayerRace()
                    {
                        GravityTolerance = new HabitationRange(-30, +20),
                        TemperatureTolerance = new HabitationRange(-50, +10),
                        RadiationTolerance = new HabitationRange(-20, +25)
                    };

                this.GameState = new GameState { Galaxy = galaxy, CurrentPlayerNum = 0, PlayerRaces = new[] { race } };
            }

            this.MapPanelControlViewModel = ViewModelLocator.MapPanelControl;
            this.SummaryPanelControlViewModel = ViewModelLocator.SummaryPanelControl;
        }

        #endregion

        #region Public properties

        public GameState GameState { get; set; }

        public MapPanelControlViewModel MapPanelControlViewModel
        {
            get
            {
                return this.mapPanelControlViewModel;
            }

            private set
            {
                this.Set(() => this.MapPanelControlViewModel, ref this.mapPanelControlViewModel, value);
            }
        }

        public SummaryPanelControlViewModel SummaryPanelControlViewModel
        {
            get
            {
                return this.summaryPanelControlViewModel;
            }

            private set
            {
                this.Set(() => this.SummaryPanelControlViewModel, ref this.summaryPanelControlViewModel, value);
            }
        }

        #endregion
    }
}