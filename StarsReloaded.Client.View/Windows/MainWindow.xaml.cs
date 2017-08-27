namespace StarsReloaded.View.Windows
{
    using System.Windows;
    using GalaSoft.MvvmLight.Messaging;
    using StarsReloaded.Client.Mediation;
    using StarsReloaded.Client.Mediation.Messages;
    using StarsReloaded.Client.Mediation.Windows;
    using StarsReloaded.Client.ViewModel.Windows;
    using StarsReloaded.Shared.Model;

    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = IoCHelper.Resolve<MainWindowViewModel>();
        }

        public void Show(GameState gameState)
        {
            Messenger.Default.Send(new GameStateLoadedMessage(gameState));
        }
    }
}
