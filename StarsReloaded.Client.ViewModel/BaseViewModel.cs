namespace StarsReloaded.Client.ViewModel
{
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using GalaSoft.MvvmLight;

    using StarsReloaded.Client.ViewModel.Attributes;

    public class BaseViewModel : ViewModelBase
    {
        public override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.RaisePropertyChanged(propertyName);

            foreach (var property in this.GetType().GetProperties())
            {
                foreach (var baseProp in property.GetCustomAttributes(typeof(DependsUponAttribute)))
                {
                    var dependentUpon = (baseProp as DependsUponAttribute)?.PropertyName;
                    if (dependentUpon == propertyName)
                    {
                        this.RaisePropertyChanged(property.Name);
                    }
                }
            }
        }
    }
}