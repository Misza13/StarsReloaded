namespace StarsReloaded.View.Windows
{
    using System.Windows;
    using StarsReloaded.Client.ViewModel;
    using StarsReloaded.Client.ViewModel.Windows;

    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = IoCHelper.Resolve<MainWindowViewModel>();
        }
    }
}
