namespace StarsReloaded.Shared.WorldGen.Services
{
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen.Meta;

    /// <summary>
    /// Definition of galaxy-generating service.
    /// </summary>
    public interface IGalaxyGeneratorService
    {
        /// <summary>
        /// Generate a galaxy of planets. Size and density are chosen from a set of predefined constants.
        /// </summary>
        /// <param name="size">Size of the galaxy. See <see cref="GalaxySize"/>.</param>
        /// <param name="density">Density of planets in the galaxy. See <see cref="GalaxyDensity"/>.</param>
        /// <param name="planetDistribution">Planet distribution mode.</param>
        /// <returns>The generated <see cref="Galaxy"/>.</returns>
        Galaxy Generate(GalaxySize size, GalaxyDensity density, PlanetDistribution planetDistribution);
    }
}