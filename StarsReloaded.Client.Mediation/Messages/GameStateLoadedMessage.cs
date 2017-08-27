namespace StarsReloaded.Client.Mediation.Messages
{
    using StarsReloaded.Shared.Model;

    public class GameStateLoadedMessage
    {
        public GameStateLoadedMessage(GameState gameState)
        {
            this.GameState = gameState;
        }

        public GameState GameState { get; private set; }
    }
}