namespace StarsReloaded.View
{
    using System.Windows;
    using GalaSoft.MvvmLight.Messaging;
    using StarsReloaded.Client.ViewModel;
    using StarsReloaded.Client.ViewModel.Messages;
    using StarsReloaded.Client.ViewModel.Windows;
    using StarsReloaded.Shared.WorldGen;
    using StarsReloaded.View.Windows;

    public partial class App : Application
    {
        public App()
        {
            Messenger.Default.Register<ShowMainWindowMessage>(this, ShowMainWindow);
        }

        private static void ShowMainWindow(ShowMainWindowMessage message)
        {
            var mainWindow = new MainWindow();

            mainWindow.DataContext = ViewModelLocator.MainWindow;
            ////TODO: test for null and throw
            (mainWindow.DataContext as MainWindowViewModel).Galaxy = message.Galaxy;
            mainWindow.Show();
        }
    }
}
