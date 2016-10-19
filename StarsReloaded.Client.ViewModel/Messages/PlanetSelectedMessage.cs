namespace StarsReloaded.Client.ViewModel.Messages
{
    using GalaSoft.MvvmLight.Messaging;

    using StarsReloaded.Client.ViewModel.ModelWrap;

    public class PlanetSelectedMessage : MessageBase
    {
        public PlanetSelectedMessage(PlanetViewModel planet)
        {
            this.Planet = planet;
        }

        public PlanetViewModel Planet { get; private set; }
    }
}