namespace StarsReloaded.Client.ViewModel.Windows
{
    using GalaSoft.MvvmLight;
    using StarsReloaded.Client.ViewModel.Controls;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen;

    public sealed class MainWindowViewModel : ViewModelBase
    {
        private Galaxy _galaxy;

        public MainWindowViewModel()
        {
            if (IsInDesignMode)
            {
                var worldGen = new GalaxyGenerator(800, 800);
                var galaxy = worldGen.GenerateUniform(100);
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