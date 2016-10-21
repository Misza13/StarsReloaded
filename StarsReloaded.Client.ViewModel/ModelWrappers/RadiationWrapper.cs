namespace StarsReloaded.Client.ViewModel.ModelWrappers
{
    using StarsReloaded.Shared.Model;

    public class RadiationWrapper : HabitationParameterWrapper
    {
        public RadiationWrapper(HabitationParameter habitationParameter) : base(habitationParameter)
        {
        }

        public override string DisplayValue => $"{this.Clicks + 50}mR";
    }
}