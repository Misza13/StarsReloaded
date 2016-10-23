namespace StarsReloaded.View.Controls.Panels
{
    using System.Windows;
    using System.Windows.Controls;

    using StarsReloaded.Client.ViewModel.Controls;

    public partial class SummaryPanel : UserControl
    {
        public SummaryPanel()
        {
            InitializeComponent();
        }

        private void Canvas_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            (this.DataContext as SummaryPanelControlViewModel).MineralChartWidth = e.NewSize.Width;
        }
    }
}
