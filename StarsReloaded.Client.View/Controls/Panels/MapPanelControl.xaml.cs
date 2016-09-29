namespace StarsReloaded.View.Controls.Panels
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using StarsReloaded.Client.ViewModel.Controls;

    public partial class MapPanelControl : UserControl
    {
        public MapPanelControl()
        {
            this.InitializeComponent();
        }

        private void Canvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var p = Mouse.GetPosition(sender as IInputElement);
            (this.DataContext as MapPanelControlViewModel)?.MapClickCommand.Execute(p);
        }
    }
}
