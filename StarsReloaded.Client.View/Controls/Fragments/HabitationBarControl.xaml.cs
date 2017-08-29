namespace StarsReloaded.Client.View.Controls.Fragments
{
    using System.Windows;
    using System.Windows.Controls;

    using StarsReloaded.Client.Mediation;
    using StarsReloaded.Client.View.Utilities;
    using StarsReloaded.Client.ViewModel.Fragments;
    using StarsReloaded.Shared.Model;

    public partial class HabitationBarControl : UserControl
    {
        public static readonly DependencyProperty HabitationParameterTypeProperty =
            new DependencyPropertyBuilder<HabitationBarControl, HabitationParameterType>("HabitationParameterType")
                .PropagateToViewModel<HabitationBarControlViewModel>((vm, v) => vm.ParameterType = v)
                .Build();

        public static readonly DependencyProperty HabitationRangeProperty =
            new DependencyPropertyBuilder<HabitationBarControl, HabitationRange>("HabitationRange")
                .PropagateToViewModel<HabitationBarControlViewModel>((vm, v) => vm.HabitationRange = v)
                .Build();

        public static readonly DependencyProperty OriginalValueProperty =
            new DependencyPropertyBuilder<HabitationBarControl, HabitationParameter>("OriginalValue")
                .PropagateToViewModel<HabitationBarControlViewModel>((vm, v) => vm.OriginalValue = v)
                .Build();

        public static readonly DependencyProperty CurrentValueProperty =
            new DependencyPropertyBuilder<HabitationBarControl, HabitationParameter>("CurrentValue")
                .PropagateToViewModel<HabitationBarControlViewModel>((vm, v) => vm.CurrentValue = v)
                .Build();

        public static readonly DependencyProperty MaxTerraformTechProperty =
            new DependencyPropertyBuilder<HabitationBarControl, int>("MaxTerraformTech")
                .PropagateToViewModel<HabitationBarControlViewModel>((vm, v) => vm.MaxTerraformTech = v)
                .Build();

        public HabitationBarControl()
        {
            this.InitializeComponent();
            this.DataContext = IoCHelper.Resolve<HabitationBarControlViewModel>();
        }

        public HabitationParameterType HabitationParameterType
        {
            private get { return (HabitationParameterType)this.GetValue(HabitationParameterTypeProperty); }
            set { this.SetValue(HabitationParameterTypeProperty, value); }
        }

        public HabitationRange HabitationRange
        {
            private get { return (HabitationRange)this.GetValue(HabitationRangeProperty); }
            set { this.SetValue(HabitationRangeProperty, value); }
        }

        public HabitationParameter OriginalValue
        {
            private get { return (HabitationParameter)this.GetValue(OriginalValueProperty); }
            set { this.SetValue(OriginalValueProperty, value); }
        }

        public HabitationParameter CurrentValue
        {
            private get { return (HabitationParameter)this.GetValue(CurrentValueProperty); }
            set { this.SetValue(CurrentValueProperty, value); }
        }

        public int MaxTerraformTech
        {
            private get { return (int)this.GetValue(MaxTerraformTechProperty); }
            set { this.SetValue(MaxTerraformTechProperty, value); }
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            (this.DataContext as HabitationBarControlViewModel)?.SizeChangedCommand?.Execute(e.NewSize);
        }
    }
}
