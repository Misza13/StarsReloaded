namespace StarsReloaded.Client.ViewModel.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DependsUponAttribute : Attribute
    {
        public DependsUponAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }
    }
}
