namespace StarsReloaded.Client.ViewModel.Windows
{
    using System;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using StarsReloaded.Client.ViewModel.Messages;
    using StarsReloaded.Shared.WorldGen;

    public class StartupWindowViewModel : ViewModelBase
    {
        public StartupWindowViewModel()
        {
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
            var worldGen = new GalaxyGenerator(800, 800);
            var galaxy = worldGen.GenerateUniform(100);

            Messenger.Default.Send(new ShowMainWindowMessage() {Galaxy = galaxy});
            CloseAction?.Invoke();
        }

        private void Exit()
        {
            CloseAction?.Invoke();
        }
    }
}
