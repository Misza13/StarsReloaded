namespace StarsReloaded.Client.ViewModel.Windows
{
    using GalaSoft.MvvmLight.Messaging;

    using StarsReloaded.Client.ViewModel.Messages;
    using StarsReloaded.Client.ViewModel.ModelWrappers;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    public sealed class MainWindowViewModel : BaseViewModel
    {
        #region Constructors

        public MainWindowViewModel(IGalaxyGeneratorService galaxyGeneratorService)
        {
            if (this.IsInDesignMode)
            {
                var galaxy = galaxyGeneratorService.Generate(GalaxySize.Medium, GalaxyDensity.Packed, PlanetDistribution.UniformClumping);

                var race = new PlayerRace()
                    {
                        GravityTolerance = new HabitationRange(-30, +20),
                        TemperatureTolerance = new HabitationRange(-50, +10),
                        RadiationTolerance = new HabitationRange(-20, +25)
                    };

                this.GameState = new GameState { Galaxy = galaxy, CurrentPlayerNum = 0, PlayerRaces = new[] { race } };

                Messenger.Default.Send(new GameStateLoadedMessage(this.GameState));

                var selectedPlanet = galaxy.Planets.Find(p => p.X <= 200 && p.Y <= 200);
                Messenger.Default.Send(new PlanetSelectedMessage(new PlanetWrapper(selectedPlanet)));
            }
        }

        #endregion

        #region Public properties

        public GameState GameState { get; set; }

        #endregion
    }
}