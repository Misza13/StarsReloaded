namespace StarsReloaded.Client.ViewModel.Windows
{
    using System;
    using System.Windows.Threading;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using StarsReloaded.Client.Mediation;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    public class StartupWindowViewModel : BaseViewModel
    {
        #region Constructors

        public StartupWindowViewModel(IGalaxyGeneratorService galaxyGeneratorService)
        {
            this.GeneratorService = galaxyGeneratorService;

            this.NewGameCommand = new RelayCommand(this.NewGame);
            this.ExitCommand = new RelayCommand(this.Exit);
        }

        #endregion

        #region Dependencies

        public IWindowManager WindowManager { get; set; }
        public IGalaxyGeneratorService GeneratorService { get; set; }

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
            var gameState = DemoData.GenerateGameState();

            this.WindowManager.ShowMainWindow(gameState);
            this.CloseAction?.Invoke();
        }

        private void Exit()
        {
            this.CloseAction?.Invoke();
        }

        #endregion
    }
}
