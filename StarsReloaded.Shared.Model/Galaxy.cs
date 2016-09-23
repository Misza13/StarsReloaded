namespace StarsReloaded.Shared.Model
{
    using System.Collections.ObjectModel;

    public class Galaxy
    {
        public Galaxy()
        {
            Planets = new ObservableCollection<Planet>();
        }

        public ObservableCollection<Planet> Planets { get; private set; }
    }
}
