namespace StarsReloaded.Client.ViewModel.ModelWrap
{
    using StarsReloaded.Shared.Model;

    public class RadiationViewModel : HabitationParameterViewModel
    {
        public RadiationViewModel(HabitationParameter habitationParameter)
            : base(habitationParameter)
        {
        }

        public override string DisplayValue => this.Clicks.ToString() + "r"; ////TODO
    }
}