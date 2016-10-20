namespace StarsReloaded.Client.ViewModel.ModelWrap
{
    using StarsReloaded.Shared.Model;

    public abstract class HabitationParameterViewModel
    {
        #region Private fields

        private readonly HabitationParameter habitationParameter;

        #endregion

        #region Constructors

        protected HabitationParameterViewModel(HabitationParameter habitationParameter)
        {
            this.habitationParameter = habitationParameter;
        }

        #endregion

        #region Public properties

        public int Clicks => this.habitationParameter.Clicks;

        public abstract string DisplayValue { get; }

        #endregion
    }
}