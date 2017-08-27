
namespace StarsReloaded.View.Controls.Fragments
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using StarsReloaded.Client.Mediation;
    using StarsReloaded.Client.ViewModel;
    using StarsReloaded.Client.ViewModel.Fragments;

    public partial class CollapsiblePanel : UserControl
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header",
            typeof(string),
            typeof(CollapsiblePanel),
            new PropertyMetadata(default(string)));

        public CollapsiblePanel()
        {
            this.InitializeComponent();
            this.DataContext = IoCHelper.Resolve<CollapsiblePanelViewModel>();

            var bindingViewMode = new Binding(nameof(CollapsiblePanelViewModel.Header)) { Mode = BindingMode.TwoWay };
            this.SetBinding(HeaderProperty, bindingViewMode);
        }

        public string Header
        {
            get => (string)this.GetValue(HeaderProperty);
            set => this.SetValue(HeaderProperty, value);
        }
    }
}
