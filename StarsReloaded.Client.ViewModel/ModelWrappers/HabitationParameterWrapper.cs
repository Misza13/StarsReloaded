namespace StarsReloaded.Client.ViewModel.ModelWrappers
{
    using StarsReloaded.Shared.Model;

    public abstract class HabitationParameterWrapper : BaseWrapper<HabitationParameter>
    {
        protected HabitationParameterWrapper(HabitationParameter habitationParameter) : base(habitationParameter)
        {
        }

        public abstract string DisplayValue { get; }

        protected int Clicks => this.Model.Clicks;
    }
}