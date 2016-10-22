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
        private int maxTerraformTech;

        private HabitationParameter currentValue;

        private HabitationParameter originalValue;

        #endregion

        #region Constructors

        public HabitationBarControlViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.ParameterType = HabitationParameterType.Radiation;
                this.BarWidth = 400;
                this.Range = new HabitationRange(-20, +30);
                this.MaxTerraformTech = 20;
                this.CurrentValue = new HabitationParameter(+25);
                this.OriginalValue = new HabitationParameter(+40);
            }
            else
            {
                this.SizeChangedCommand = new RelayCommand<Size>(this.SizeChanged);
            }
        }

        #endregion

        #region Public properties

        public HabitationParameterType ParameterType { private get; set; }

        public HabitationRange Range
        {
            private get
            {
                return this.range;
            }

            set
            {
                this.Set(() => this.Range, ref this.range, value);
            }
        }

        public int MaxTerraformTech
        {
            private get
            {
                return this.maxTerraformTech;
            }

            set
            {
                this.Set(() => this.MaxTerraformTech, ref this.maxTerraformTech, value);
            }
        }

        public HabitationParameter CurrentValue
        {
            get
            {
                return this.currentValue;
            }

            set
            {
                this.Set(() => this.CurrentValue, ref this.currentValue, value);
            }
        }

        public HabitationParameter OriginalValue
        {
            get
            {
                return this.originalValue;
            }

            set
            {
                this.Set(() => this.OriginalValue, ref this.originalValue, value);
            }
        }

        public double BarWidth
        {
            private get
            {
                return this.barWidth;
            }

            set
            {
                this.Set(() => this.BarWidth, ref this.barWidth, value);
            }
        }

        [DependsUpon(nameof(CurrentValue))]
        public int CurrentValueClicks => this.CurrentValue?.Clicks ?? 0;

        [DependsUpon(nameof(OriginalValue))]
        public int OriginalValueClicks => this.OriginalValue?.Clicks ?? 0;

        [DependsUpon(nameof(Range))]
        public Visibility HabBarVisibility => (this.Range?.IsImmune ?? true) ? Visibility.Hidden : Visibility.Visible;

        [DependsUpon(nameof(Range))]
        [DependsUpon(nameof(BarWidth))]
        public double HabBarLeft => (this.Range?.IsImmune ?? true)
            ? 0
            : this.BarWidth * (this.Range.MinValue.Clicks + 50) / 100;

        [DependsUpon(nameof(Range))]
        [DependsUpon(nameof(BarWidth))]
        public double HabBarWidth => (this.Range?.IsImmune ?? true)
            ? 0
            : this.BarWidth * (this.Range.MaxValue.Clicks - this.Range.MinValue.Clicks) / 100;

        [DependsUpon(nameof(ParameterType))]
        public string BarFillColor => this.GetBarFillColor();

        [DependsUpon(nameof(CurrentValue))]
        public Visibility CurrentValueVisibility => (this.CurrentValue == null) ? Visibility.Hidden : Visibility.Visible;

        [DependsUpon(nameof(BarWidth))]
        [DependsUpon(nameof(CurrentValueClicks))]
        public double CurrentValuePos => this.BarWidth * (this.CurrentValueClicks + 50) / 100;

        [DependsUpon(nameof(OriginalValue))]
        public Visibility OriginalValueVisibility => (this.OriginalValue == null) ? Visibility.Hidden : Visibility.Visible;

        [DependsUpon(nameof(BarWidth))]
        [DependsUpon(nameof(OriginalValueClicks))]
        public double OriginalValuePos => this.BarWidth * (this.OriginalValueClicks + 50) / 100;

        [DependsUpon(nameof(BarWidth))]
        [DependsUpon(nameof(Range))]
        [DependsUpon(nameof(OriginalValueClicks))]
        [DependsUpon(nameof(MaxTerraformTech))]
        public double AfterTerraformPos
            =>
            this.BarWidth * (GetBestTerraformClicks(this.Range, this.OriginalValueClicks, this.MaxTerraformTech) + 50) / 100;

        [DependsUpon(nameof(ParameterType))]
        public string GraphStrokeColor => this.GetGraphStrokeColor();

        #endregion

        #region Commands

        public RelayCommand<Size> SizeChangedCommand { get; private set; }

        #endregion

        #region Private methods

        private static int GetBestTerraformClicks(HabitationRange range, int original, int maxTech)
        {
            if (range?.IsImmune ?? true)
            {
                return original;
            }

            var midpoint = (range.MinValue.Clicks + range.MaxValue.Clicks) / 2;
            if (original > midpoint)
            {
                return Math.Max(original - maxTech, midpoint);
            }
            else
            {
                return Math.Min(original + maxTech, midpoint);
            }
        }

        private string GetBarFillColor()
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

        private string GetGraphStrokeColor()
        {
            switch (this.ParameterType)
            {
                case HabitationParameterType.Gravity:
                    return "Blue";
                case HabitationParameterType.Temperature:
                    return "Red";
                case HabitationParameterType.Radiation:
                    return "LightGreen";
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
