namespace StarsReloaded.View.Controls.Panels
{
    using System.Windows;
    using System.Windows.Controls;
    using StarsReloaded.Shared.Model;

    public partial class MapPanelControl : UserControl
    {
        public MapPanelControl()
        {
            InitializeComponent();

            ////(this.DataContext as MapPanelControlViewModel).Galaxy = Galaxy;
        }

        public static readonly DependencyProperty GalaxyProperty = DependencyProperty.Register(
            "Galaxy", typeof(Galaxy), typeof(MapPanelControl), new PropertyMetadata(default(Galaxy)));

        public Galaxy Galaxy
        {
            get { return (Galaxy) GetValue(GalaxyProperty); }
            set { SetValue(GalaxyProperty, value); }
        }
    }
}
