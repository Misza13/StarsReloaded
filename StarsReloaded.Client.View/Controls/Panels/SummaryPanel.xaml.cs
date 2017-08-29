namespace StarsReloaded.Client.View.Controls.Panels
{
    using System.Windows.Controls;

    using StarsReloaded.Client.Mediation;
    using StarsReloaded.Client.ViewModel.Controls;

    public partial class SummaryPanel : UserControl
    {
        public SummaryPanel()
        {
            this.InitializeComponent();
            this.DataContext = IoCHelper.Resolve<SummaryPanelViewModel>();
        }
    }
}
