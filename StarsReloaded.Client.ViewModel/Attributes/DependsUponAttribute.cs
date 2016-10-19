namespace StarsReloaded.Client.ViewModel.Attributes
{
    using System;

    public class DependsUponAttribute : Attribute
    {
        public DependsUponAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }
    }
}
