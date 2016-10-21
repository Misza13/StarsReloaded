namespace StarsReloaded.View.Controls.Fragments
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using StarsReloaded.Client.ViewModel.Fragments;

    public partial class HabitationBarControl : UserControl
    {
        public HabitationBarControl()
        {
            this.InitializeComponent();
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            (this.DataContext as HabitationBarControlViewModel)?.SizeChangedCommand.Execute(e.NewSize);
        }
    }
}
