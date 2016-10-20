namespace StarsReloaded.Client.ViewModel.ModelWrap
{
    using StarsReloaded.Shared.Model;

    public class GravityViewModel : HabitationParameterViewModel
    {
        private readonly decimal[] lowValues =
            {
                0.12m, 0.12m, 0.13m, 0.13m, 0.14m, 0.14m, 0.15m, 0.15m, 0.16m, 0.17m,
                0.17m, 0.18m, 0.19m, 0.20m, 0.21m, 0.22m, 0.24m, 0.25m, 0.27m, 0.29m,
                0.31m, 0.33m, 0.36m, 0.40m, 0.44m, 0.50m, 0.51m, 0.52m, 0.53m, 0.54m,
                0.55m, 0.56m, 0.58m, 0.59m, 0.60m, 0.62m, 0.64m, 0.65m, 0.67m, 0.69m,
                0.71m, 0.73m, 0.75m, 0.78m, 0.80m, 0.83m, 0.86m, 0.89m, 0.92m, 0.96m
            };

        public GravityViewModel(HabitationParameter habitationParameter)
            : base(habitationParameter)
        {
        }

        public override string DisplayValue => $"{this.ToValue()}g";

        private decimal ToValue()
        {
            if (this.Clicks < 0)
            {
                return this.lowValues[this.Clicks + 50];
            }

            if (this.Clicks < 25)
            {
                return 1.00m + (0.04m * this.Clicks);
            }

            return 2.00m + (0.24m * (this.Clicks - 25));
        }
    }
}