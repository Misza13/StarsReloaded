namespace StarsReloaded.Shared.WorldGen.Services
{
    using StarsReloaded.Shared.Model;

    public interface IPlanetGeneratorService
    {
        void PopulatePlanetStats(Planet planet);
    }
}