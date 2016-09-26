namespace StarsReloaded.Client.ViewModel.Windows
{
    using GalaSoft.MvvmLight;
    using StarsReloaded.Client.ViewModel.Controls;

    public sealed class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            if (IsInDesignMode)
            {
                MapPanelControlViewModel = new MapPanelControlViewModel();
            }
            else
            {
                ////TODO: Actual logic
                MapPanelControlViewModel = new MapPanelControlViewModel();
            }
        }

        public MapPanelControlViewModel MapPanelControlViewModel { get; private set; }
    }
}