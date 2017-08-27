namespace StarsReloaded.Client.ViewModel
{
    using GalaSoft.MvvmLight.Messaging;
    using StarsReloaded.Client.ViewModel.Messages;
    using StarsReloaded.Shared.Model;

    public interface IWindowManager
    {
        void ShowStartupWindow();

        void ShowMainWindow(GameState gameState);
    }

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

    public interface IWindow
    {
        void Show();
    }

    public interface IMainWindow : IWindow
    {
    }

    public interface IStartupWindow : IWindow
    {
    }
}