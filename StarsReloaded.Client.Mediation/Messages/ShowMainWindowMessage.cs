namespace StarsReloaded.Client.Mediation.Messages
{
    using GalaSoft.MvvmLight.Messaging;
    using StarsReloaded.Shared.Model;

    public class ShowMainWindowMessage : MessageBase
    {
        public ShowMainWindowMessage(GameState gameState)
        {
            this.GameState = gameState;
        }

        public GameState GameState { get; private set; }
    }
}