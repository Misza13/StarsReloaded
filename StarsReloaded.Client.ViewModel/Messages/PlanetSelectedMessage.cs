namespace StarsReloaded.Client.ViewModel.Messages
{
    using GalaSoft.MvvmLight.Messaging;

    using StarsReloaded.Client.ViewModel.ModelWrappers;

    public class PlanetSelectedMessage : MessageBase
    {
        public PlanetSelectedMessage(PlanetWrapper planet)
        {
            this.Planet = planet;
        }

        public PlanetWrapper Planet { get; private set; }
    }
}