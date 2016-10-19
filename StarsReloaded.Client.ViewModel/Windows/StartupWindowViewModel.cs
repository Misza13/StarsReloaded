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
        #region Private fields

        private readonly IGalaxyGeneratorService galaxyGeneratorService;

        #endregion

        #region Constructors

        public StartupWindowViewModel(IGalaxyGeneratorService galaxyGeneratorService)
        {
            this.galaxyGeneratorService = galaxyGeneratorService;

            this.NewGameCommand = new RelayCommand(this.NewGame);
            this.ExitCommand = new RelayCommand(this.Exit);
        }

        #endregion

        #region Commands

        public RelayCommand NewGameCommand { get; set; }

        public RelayCommand OpenGameCommand { get; set; }

        public RelayCommand ContinueGameCommand { get; set; }

        public RelayCommand ExitCommand { get; set; }

        #endregion

        #region Actions

        public Action CloseAction { private get; set; }

        #endregion

        #region Private methods

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

        #endregion
    }
}
