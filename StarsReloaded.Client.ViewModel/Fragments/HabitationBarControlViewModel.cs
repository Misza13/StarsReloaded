namespace StarsReloaded.Client.ViewModel.Fragments
{
    using System;
    using System.Windows;

    using GalaSoft.MvvmLight.Command;

    using StarsReloaded.Client.ViewModel.Attributes;
    using StarsReloaded.Shared.Model;

    public class HabitationBarControlViewModel : BaseViewModel
    {
        #region Private fields

        private HabitationRange range;
        private double barWidth;

        #endregion

        #region Constructors

        public HabitationBarControlViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.ParameterType = HabitationParameterType.Radiation;
                this.BarWidth = 400;
                this.Range = new HabitationRange(-20, +30);
                this.CurrentValue = new HabitationParameter(+15);
                this.OriginalValue = new HabitationParameter(+45);
            }
            else
            {
                this.SizeChangedCommand = new RelayCommand<Size>(this.SizeChanged);
            }
        }

        #endregion

        #region Public properties

        public HabitationParameterType ParameterType { get; set; }

        public HabitationRange Range
        {
            private get
            {
                return this.range;
            }

            set
            {
                if (value != this.range)
                {
                    this.range = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public HabitationParameter CurrentValue { get; set; }

        public HabitationParameter OriginalValue { get; set; }

        public double BarWidth
        {
            private get
            {
                return this.barWidth;
            }

            set
            {
                if (value != this.barWidth)
                {
                    this.barWidth = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        [DependsUpon(nameof(CurrentValue))]
        public int CurrentValueClicks => this.CurrentValue.Clicks;

        [DependsUpon(nameof(OriginalValue))]
        public int OriginalValueClicks => this.OriginalValue.Clicks;

        [DependsUpon(nameof(Range))]
        public Visibility HabBarVisibility => (this.Range?.IsImmune ?? true) ? Visibility.Hidden : Visibility.Visible;

        [DependsUpon(nameof(Range))]
        [DependsUpon(nameof(BarWidth))]
        public double HabBarLeft => (this.Range?.IsImmune ?? true)
            ? 0
            : this.BarWidth * (this.Range.MinValue.Clicks + 50d) / 100;

        [DependsUpon(nameof(Range))]
        [DependsUpon(nameof(BarWidth))]
        public double HabBarWidth => (this.Range?.IsImmune ?? true)
            ? 0
            : this.BarWidth * (this.Range.MaxValue.Clicks - this.Range.MinValue.Clicks) / 100;

        [DependsUpon(nameof(ParameterType))]
        public string FillColor => this.GetFillColor();

        #endregion

        #region Commands

        public RelayCommand<Size> SizeChangedCommand { get; private set; }

        #endregion

        #region Private methods

        private string GetFillColor()
        {
            switch (this.ParameterType)
            {
                case HabitationParameterType.Gravity:
                    return "DarkBlue";
                case HabitationParameterType.Temperature:
                    return "DarkRed";
                case HabitationParameterType.Radiation:
                    return "DarkGreen";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SizeChanged(Size newSize)
        {
            this.BarWidth = newSize.Width;
        }

        #endregion
    }
}
