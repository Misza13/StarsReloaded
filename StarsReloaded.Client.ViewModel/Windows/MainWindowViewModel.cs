namespace StarsReloaded.Client.ViewModel.Windows
{
    using GalaSoft.MvvmLight.Messaging;
    using StarsReloaded.Client.Mediation.Messages;
    using StarsReloaded.Client.ViewModel.ModelWrappers;
    using StarsReloaded.Shared.Model;

    public sealed class MainWindowViewModel : BaseViewModel
    {
        #region Constructors

        public MainWindowViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.GameState = DemoData.GenerateGameState();
                Messenger.Default.Send(new GameStateLoadedMessage(this.GameState));

                var selectedPlanet = this.GameState.Galaxy.Planets.Find(p => p.X <= 200 && p.Y <= 200);
                Messenger.Default.Send(new PlanetSelectedMessage(selectedPlanet));
            }
        }

        #endregion

        #region Public properties

        public GameState GameState { get; set; }

        #endregion
    }
}