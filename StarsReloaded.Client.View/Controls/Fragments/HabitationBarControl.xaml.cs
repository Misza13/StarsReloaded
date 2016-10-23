namespace StarsReloaded.View.Controls.Fragments
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    using StarsReloaded.Client.ViewModel.Fragments;
    using StarsReloaded.Shared.Model;

    public partial class HabitationBarControl : UserControl
    {
        public static readonly DependencyProperty HabitationParameterTypeProperty =
            DependencyProperty.Register(
                "HabitationParameterType",
                typeof(HabitationParameterType),
                typeof(HabitationBarControl),
                new PropertyMetadata(default(HabitationParameterType)));

        public HabitationBarControl()
        {
            this.InitializeComponent();

            var bindingViewMode = new Binding(nameof(HabitationBarControlViewModel.ParameterType)) { Mode = BindingMode.OneWay };
            this.SetBinding(HabitationParameterTypeProperty, bindingViewMode);
        }

        public HabitationParameterType HabitationParameterType
        {
            private get
            {
                return (HabitationParameterType)this.GetValue(HabitationParameterTypeProperty);
            }

            set
            {
                this.SetValue(HabitationParameterTypeProperty, value);
            }
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            (this.DataContext as HabitationBarControlViewModel)?.SizeChangedCommand?.Execute(e.NewSize);
        }
    }
}
