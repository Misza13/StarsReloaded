namespace StarsReloaded.Client.ViewModel.ModelWrap
{
    using StarsReloaded.Shared.Model;

    public class GravityViewModel : HabitationParameterViewModel
    {
        public GravityViewModel(HabitationParameter habitationParameter)
            : base(habitationParameter)
        {
        }

        public override string DisplayValue => this.Clicks.ToString() + "g"; ////TODO
    }
}