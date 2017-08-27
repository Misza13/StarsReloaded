namespace StarsReloaded.View.Windows
{
    using System.Windows;
    using StarsReloaded.Client.Mediation;
    using StarsReloaded.Client.Mediation.Windows;
    using StarsReloaded.Client.ViewModel;
    using StarsReloaded.Client.ViewModel.Windows;

    public partial class StartupWindow : Window, IStartupWindow
    {
        public StartupWindow()
        {
            this.InitializeComponent();
            this.DataContext = IoCHelper.Resolve<StartupWindowViewModel>();
            (this.DataContext as StartupWindowViewModel).CloseAction = this.Close;
        }
    }
}
