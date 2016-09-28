namespace StarsReloaded.Client.ViewModel.Windows
{
    using System;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using StarsReloaded.Client.ViewModel.Messages;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    public class StartupWindowViewModel : ViewModelBase
    {
        private readonly IGalaxyGeneratorService _galaxyGeneratorService;

        public StartupWindowViewModel(IGalaxyGeneratorService galaxyGeneratorService)
        {
            _galaxyGeneratorService = galaxyGeneratorService;

            this.NewGameCommand = new RelayCommand(NewGame);
            this.ExitCommand = new RelayCommand(this.Exit);
        }

        public RelayCommand NewGameCommand { get; set; }

        public RelayCommand OpenGameCommand { get; set; }

        public RelayCommand ContinueGameCommand { get; set; }

        public RelayCommand ExitCommand { get; set; }

        public Action CloseAction { private get; set; }

        private void NewGame()
        {
            ////TODO: Game init logic here
            //var galaxy = _galaxyGeneratorService.GenerateUniform(800, 800, 100);
            var galaxy = _galaxyGeneratorService.GenerateUniform(GalaxySize.Medium, GalaxyDensity.Normal);

            Messenger.Default.Send(new ShowMainWindowMessage() {Galaxy = galaxy});
            CloseAction?.Invoke();
        }

        private void Exit()
        {
            CloseAction?.Invoke();
        }
    }
}
