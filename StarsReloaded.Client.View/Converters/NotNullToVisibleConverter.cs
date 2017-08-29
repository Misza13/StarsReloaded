namespace StarsReloaded.Client.View.Converters
{
    public class NotNullToVisibleConverter : ConditionalVisibilityConverter<object>
    {
        public NotNullToVisibleConverter()
            : base(o => o != null)
        {
        }
    }
}