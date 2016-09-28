namespace StarsReloaded.Client.ViewModel.Windows
{
    using GalaSoft.MvvmLight;
    using StarsReloaded.Client.ViewModel.Controls;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    public sealed class MainWindowViewModel : ViewModelBase
    {
        private Galaxy _galaxy;

        public MainWindowViewModel(IGalaxyGeneratorService galaxyGeneratorService)
        {
            if (IsInDesignMode)
            {
                //var galaxy = galaxyGeneratorService.GenerateUniform(800, 800, 100);
                var galaxy = galaxyGeneratorService.GenerateUniform(GalaxySize.Medium, GalaxyDensity.Packed);
                Initialize(galaxy);
            }
        }

        public Galaxy Galaxy
        {
            set
            {
                if (value != _galaxy)
                {
                    _galaxy = value;
                    Initialize(_galaxy);
                    RaisePropertyChanged(nameof(MapPanelControlViewModel));
                }
            }
        }

        public MapPanelControlViewModel MapPanelControlViewModel { get; private set; }

        private void Initialize(Galaxy galaxy)
        {
            MapPanelControlViewModel = ViewModelLocator.MapPanelControl;
            MapPanelControlViewModel.Galaxy = galaxy;
        }
    }
}