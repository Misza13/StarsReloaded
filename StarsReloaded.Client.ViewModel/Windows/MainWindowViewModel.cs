namespace StarsReloaded.Client.ViewModel.Windows
{
    using StarsReloaded.Client.ViewModel.Attributes;
    using StarsReloaded.Client.ViewModel.Controls;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    public sealed class MainWindowViewModel : BaseViewModel
    {
        private Galaxy galaxy;

        public MainWindowViewModel(IGalaxyGeneratorService galaxyGeneratorService)
        {
            if (this.IsInDesignMode)
            {
                this.galaxy = galaxyGeneratorService.Generate(GalaxySize.Medium, GalaxyDensity.Packed, PlanetDistribution.UniformClumping);
                this.Initialize(this.galaxy);
            }
        }

        public Galaxy Galaxy
        {
            set
            {
                if (value != this.galaxy)
                {
                    this.galaxy = value;
                    this.Initialize(this.galaxy);
                    this.RaisePropertyChanged(nameof(this.Galaxy));
                }
            }
        }

        [DependsUpon(nameof(Galaxy))]
        public MapPanelControlViewModel MapPanelControlViewModel { get; private set; }

        private void Initialize(Galaxy galaxy)
        {
            this.MapPanelControlViewModel = ViewModelLocator.MapPanelControl;
            this.MapPanelControlViewModel.Galaxy = galaxy;
        }
    }
}