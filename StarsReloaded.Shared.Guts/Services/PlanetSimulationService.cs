namespace StarsReloaded.Shared.Guts.Services
{
    using StarsReloaded.Shared.Model;

    public class PlanetSimulationService : IPlanetSimulationService
    {
        public int GetMiningRate(Planet planet, MineralType mineralType)
        {
            ////TODO
            return planet.GetSurfaceMinerals(mineralType) / 10;
        }
    }
}
