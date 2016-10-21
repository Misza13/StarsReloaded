namespace StarsReloaded.Client.ViewModel.ModelWrappers
{
    using StarsReloaded.Shared.Model;

    public class TemperatureWrapper : HabitationParameterWrapper
    {
        public TemperatureWrapper(HabitationParameter habitationParameter) : base(habitationParameter)
        {
        }

        public override string DisplayValue => $"{this.Clicks * 4}°C";
    }
}