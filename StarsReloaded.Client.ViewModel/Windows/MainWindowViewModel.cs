namespace StarsReloaded.Client.ViewModel.Windows
{
    using GalaSoft.MvvmLight;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen;

    public sealed class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            if (IsInDesignMode)
            {
                var worldGen = new GalaxyGenerator(400, 400);
                Galaxy = worldGen.GenerateUniform(20);
            }
            else
            {
                var worldGen = new GalaxyGenerator(400, 400);
                Galaxy = worldGen.GenerateUniform(100);
            }
        }

        public Galaxy Galaxy { get; private set; }
    }
}