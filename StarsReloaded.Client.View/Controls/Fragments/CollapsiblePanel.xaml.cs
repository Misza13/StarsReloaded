
namespace StarsReloaded.View.Controls.Fragments
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

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

            var bindingViewMode = new Binding(nameof(CollapsiblePanelViewModel.Header)) { Mode = BindingMode.TwoWay };
            this.SetBinding(HeaderProperty, bindingViewMode);
        }

        public string Header
        {
            get
            {
                return (string)this.GetValue(HeaderProperty);
            }

            set
            {
                this.SetValue(HeaderProperty, value);
            }
        }

        private void CollapseButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (this.DataContext as CollapsiblePanelViewModel).CollapseCommand.Execute(null);
        }

        private void ExpandButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (this.DataContext as CollapsiblePanelViewModel).ExpandCommand.Execute(null);
        }
    }
}
