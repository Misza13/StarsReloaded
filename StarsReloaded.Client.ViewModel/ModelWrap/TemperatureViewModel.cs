namespace StarsReloaded.Client.ViewModel.ModelWrap
{
    using StarsReloaded.Shared.Model;

    public class TemperatureViewModel : HabitationParameterViewModel
    {
        public TemperatureViewModel(HabitationParameter habitationParameter)
            : base(habitationParameter)
        {
        }

        public override string DisplayValue => $"{this.Clicks * 4}°C";
    }
}