namespace StarsReloaded.Client.ViewModel.Windows
{
    using StarsReloaded.Client.ViewModel.Attributes;
    using StarsReloaded.Client.ViewModel.Controls;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    public sealed class MainWindowViewModel : BaseViewModel
    {
        #region Private fields

        private Galaxy galaxy;

        #endregion

        #region Constructors

        public MainWindowViewModel(IGalaxyGeneratorService galaxyGeneratorService)
        {
            if (this.IsInDesignMode)
            {
                this.galaxy = galaxyGeneratorService.Generate(GalaxySize.Medium, GalaxyDensity.Packed, PlanetDistribution.UniformClumping);
                this.Initialize(this.galaxy);
            }
        }

        #endregion

        #region Public properties

        public Galaxy Galaxy
        {
            set
            {
                if (value != this.galaxy)
                {
                    this.galaxy = value;
                    this.Initialize(value);
                    this.RaisePropertyChanged();
                }
            }
        }

        [DependsUpon(nameof(Galaxy))]
        public MapPanelControlViewModel MapPanelControlViewModel { get; private set; }

        public SummaryPanelControlViewModel SummaryPanelControlViewModel { get; private set; }

        #endregion

        #region Private methods

        private void Initialize(Galaxy newGalaxy)
        {
            this.MapPanelControlViewModel = ViewModelLocator.MapPanelControl;
            this.MapPanelControlViewModel.Galaxy = newGalaxy;
            this.RaisePropertyChanged(nameof(this.MapPanelControlViewModel));

            this.SummaryPanelControlViewModel = ViewModelLocator.SummaryPanelControl;
            this.RaisePropertyChanged(nameof(this.SummaryPanelControlViewModel));
        }

        #endregion
    }
}