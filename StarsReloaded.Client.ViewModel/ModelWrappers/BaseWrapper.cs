namespace StarsReloaded.Client.ViewModel.ModelWrappers
{
    using GalaSoft.MvvmLight;

    public abstract class BaseWrapper<T> : ObservableObject
    {
        protected BaseWrapper(T model)
        {
            this.Model = model;
        }

        public T Model { get; private set; }
    }
}
