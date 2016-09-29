namespace StarsReloaded.View.Windows
{
    using System.Windows;
    using StarsReloaded.Client.ViewModel.Windows;

    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            this.InitializeComponent();
        }

        private void StartupWindow_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = this.DataContext as StartupWindowViewModel;
            if (vm != null)
            {
                vm.CloseAction = this.Close;
            }
        }
    }
}
