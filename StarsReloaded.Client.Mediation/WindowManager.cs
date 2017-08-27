namespace StarsReloaded.Client.Mediation
{
    using GalaSoft.MvvmLight.Messaging;
    using StarsReloaded.Client.Mediation.Messages;
    using StarsReloaded.Client.Mediation.Windows;
    using StarsReloaded.Shared.Model;

    public class WindowManager : IWindowManager
    {
        public void ShowStartupWindow()
        {
            var window = IoCHelper.Resolve<IStartupWindow>();
            window.Show();
        }

        public void ShowMainWindow(GameState gameState)
        {
            var window = IoCHelper.Resolve<IMainWindow>();
            Messenger.Default.Send(new GameStateLoadedMessage(gameState));
            window.Show();
        }
    }
}