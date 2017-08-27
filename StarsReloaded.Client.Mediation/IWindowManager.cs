namespace StarsReloaded.Client.Mediation
{
    using StarsReloaded.Shared.Model;

    public interface IWindowManager
    {
        void ShowStartupWindow();

        void ShowMainWindow(GameState gameState);
    }
}