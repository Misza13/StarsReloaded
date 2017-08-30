namespace StarsReloaded.Client.View.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class ConditionalVisibilityConverter<T> : IValueConverter
    {
        private readonly Func<T, bool> predicate;

        public ConditionalVisibilityConverter(Func<T, bool> predicate)
        {
            this.predicate = predicate;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.predicate((T)value) ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}