namespace StarsReloaded.View.Controls.Panels
{
    using System.Windows.Controls;
    using StarsReloaded.Client.Mediation;
    using StarsReloaded.Client.ViewModel.Controls;

    public partial class GalaxyMapPanel : UserControl
    {
        public GalaxyMapPanel()
        {
            this.InitializeComponent();
            this.DataContext = IoCHelper.Resolve<GalaxyMapPanelViewModel>();
        }
    }
}
