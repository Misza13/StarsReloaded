namespace StarsReloaded.Client.ViewModel.ModelWrappers
{
    using StarsReloaded.Shared.Model;

    public abstract class HabitationParameterWrapper
    {
        #region Private fields

        private readonly HabitationParameter habitationParameter;

        #endregion

        #region Constructors

        protected HabitationParameterWrapper(HabitationParameter habitationParameter)
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