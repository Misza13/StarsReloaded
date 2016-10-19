namespace StarsReloaded.Client.ViewModel.Windows
{
    using System;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using StarsReloaded.Client.ViewModel.Messages;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    public class StartupWindowViewModel : BaseViewModel
    {
        private readonly IGalaxyGeneratorService galaxyGeneratorService;

        public StartupWindowViewModel(IGalaxyGeneratorService galaxyGeneratorService)
        {
            this.galaxyGeneratorService = galaxyGeneratorService;

            this.NewGameCommand = new RelayCommand(this.NewGame);
            this.ExitCommand = new RelayCommand(this.Exit);
        }

        public RelayCommand NewGameCommand { get; set; }

        public RelayCommand OpenGameCommand { get; set; }

        public RelayCommand ContinueGameCommand { get; set; }

        public RelayCommand ExitCommand { get; set; }

        public Action CloseAction { private get; set; }

        private void NewGame()
        {
            // TODO: Game init logic here
            var galaxy = this.galaxyGeneratorService.Generate(GalaxySize.Medium, GalaxyDensity.Normal, PlanetDistribution.UniformClumping);

            Messenger.Default.Send(new ShowMainWindowMessage() { Galaxy = galaxy });
            this.CloseAction?.Invoke();
        }

        private void Exit()
        {
            this.CloseAction?.Invoke();
        }
    }
}
