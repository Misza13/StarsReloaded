namespace StarsReloaded.View
{
    using System;
    using System.Windows;
    using GalaSoft.MvvmLight.Messaging;

    using StarsReloaded.Client.ViewModel.Messages;
    using StarsReloaded.Client.ViewModel.Windows;
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

            var vm = mainWindow.DataContext as MainWindowViewModel;
            if (vm != null)
            {
                vm.Initialize(message.Galaxy, message.Race);
            }
            else
            {
                throw new Exception("DataContext not initialized on MainWindow.");
            }

            mainWindow.Show();
        }
    }
}
