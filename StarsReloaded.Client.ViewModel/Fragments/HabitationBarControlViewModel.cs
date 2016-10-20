namespace StarsReloaded.Client.ViewModel.Fragments
{
    using System;

    using StarsReloaded.Client.ViewModel.Attributes;
    using StarsReloaded.Shared.Model;

    public class HabitationBarControlViewModel : BaseViewModel
    {
        private HabitationRange range;

        public HabitationBarControlViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.ParameterType = HabitationParameterType.Radiation;
                this.Range = new HabitationRange(-20, +30);
                this.CurrentValue = new HabitationParameter(+15);
                this.OriginalValue = new HabitationParameter(+45);
            }
        }

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

        [DependsUpon(nameof(CurrentValue))]
        public int CurrentValueClicks => this.CurrentValue.Clicks;

        [DependsUpon(nameof(OriginalValue))]
        public int OriginalValueClicks => this.OriginalValue.Clicks;

        [DependsUpon(nameof(Range))]
        public string LeftMarginWidth => (this.Range?.IsImmune ?? true)
            ? "1*"
            : ((this.Range?.MinValue.Clicks) + 50) + "*";

        [DependsUpon(nameof(Range))]
        public string BarWidth => (this.Range?.IsImmune ?? true)
            ? "0"
            : ((this.Range?.MaxValue.Clicks) - (this.Range?.MinValue.Clicks)) + "*";

        [DependsUpon(nameof(Range))]
        public string RightMarginWidth => (this.Range?.IsImmune ?? true)
            ? "0"
            : (50 - (this.Range?.MaxValue.Clicks)) + "*";

        [DependsUpon(nameof(ParameterType))]
        public string FillColor => this.GetFillColor();

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
    }
}
