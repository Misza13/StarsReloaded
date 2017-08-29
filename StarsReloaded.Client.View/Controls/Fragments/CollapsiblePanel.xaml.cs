
namespace StarsReloaded.View.Controls.Fragments
{
    using System.Windows;
    using System.Windows.Controls;
    using StarsReloaded.Client.Mediation;
    using StarsReloaded.Client.ViewModel.Fragments;
    using StarsReloaded.View.Utilities;

    public partial class CollapsiblePanel : UserControl
    {
        public static readonly DependencyProperty HeaderProperty =
            new DependencyPropertyBuilder<CollapsiblePanel, string>("Header")
                .PropagateToViewModel<CollapsiblePanelViewModel>((vm, v) => vm.Header = v)
                .Build();

        public CollapsiblePanel()
        {
            this.InitializeComponent();
            this.DataContext = IoCHelper.Resolve<CollapsiblePanelViewModel>();
        }

        public string Header
        {
            get { return (string)this.GetValue(HeaderProperty); }
            set { this.SetValue(HeaderProperty, value); }
        }
    }
}
