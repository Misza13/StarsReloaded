namespace StarsReloaded.View.Utilities
{
    using System;
    using System.Windows;

    public class DependencyPropertyBuilder<TOwner, TPropType>
    {
        private readonly string propertyName;
        private TPropType defaultValue = default(TPropType);
        private PropertyChangedCallback propertyChangedCallback = null;

        public DependencyPropertyBuilder(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public DependencyProperty Build()
        {
            var metadata = new PropertyMetadata(this.defaultValue, this.propertyChangedCallback,
                (o, value) =>
                    {
                        return value;
                    });
            return DependencyProperty.Register(this.propertyName, typeof(TPropType), typeof(TOwner), metadata);
        }

        public DependencyPropertyBuilder<TOwner, TPropType> WithDefaultValue(TPropType value)
        {
            this.defaultValue = value;

            return this;
        }

        public DependencyPropertyBuilder<TOwner, TPropType> PropagateToViewModel<TViewModel>(
            Action<TViewModel, TPropType> propertySetter)
        {
            this.propertyChangedCallback = (o, args) =>
                {
                    var vm = (TViewModel)((FrameworkElement)o).DataContext;
                    if (vm == null)
                    {
                        return;
                    }

                    var value = (TPropType)args.NewValue;

                    propertySetter(vm, value);
                };

            return this;
        }
    }
}