namespace StarsReloaded.Client.ViewModel
{
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.Services;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    public static class DemoData
    {
        public static GameState GenerateGameState()
        {
            var rng = new RngService();
            var galaxyGeneratorService = new GalaxyGeneratorService(rng, new PlanetGeneratorService(rng));

            var galaxy = galaxyGeneratorService.Generate(GalaxySize.Medium, GalaxyDensity.Packed, PlanetDistribution.UniformClumping);

            var race = new PlayerRace()
            {
                GravityTolerance = new HabitationRange(-30, +20),
                TemperatureTolerance = new HabitationRange(-50, +10),
                RadiationTolerance = new HabitationRange(-20, +25)
            };

            return new GameState { Galaxy = galaxy, CurrentPlayerNum = 0, PlayerRaces = new[] { race } };
        }
    }
}