namespace StarsReloaded.Client.Mediation.Messages
{
    using GalaSoft.MvvmLight.Messaging;
    using StarsReloaded.Shared.Model;

    public class PlanetSelectedMessage : MessageBase
    {
        public PlanetSelectedMessage(Planet planet)
        {
            this.Planet = planet;
        }

        public Planet Planet { get; private set; }
    }
}