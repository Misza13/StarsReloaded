namespace StarsReloaded.Shared.Guts.Services
{
    using StarsReloaded.Shared.Model;

    public interface IPlanetSimulationService
    {
        int GetMiningRate(Planet planet, MineralType mineralType);
    }
}