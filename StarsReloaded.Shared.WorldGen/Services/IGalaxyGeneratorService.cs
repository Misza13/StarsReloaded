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
        /// Generate a galaxy with uniformly distributed planets.
        /// Size and density are chosen from a set of predefined constants.
        /// </summary>
        /// <param name="size">
        /// Size of the galaxy. See <see cref="GalaxySize"/>.
        /// </param>
        /// <param name="density">
        /// Density of planets in the galaxy. See <see cref="GalaxyDensity"/>.
        /// </param>
        /// <returns>
        /// The generated <see cref="Galaxy"/>.
        /// </returns>
        Galaxy GenerateUniform(GalaxySize size, GalaxyDensity density);

        /// <summary>
        /// Generate a galaxy with uniformly distributed planets.
        /// Size and density are chosen from a set of predefined constants.
        /// </summary>
        /// <param name="width">
        /// The width (X axis) of the galaxy.
        /// </param>
        /// <param name="height">
        /// The height (Y axis) of the galaxy.
        /// </param>
        /// <param name="numPlanets">
        /// Number of planets in the galaxy.
        /// </param>
        /// <returns>
        /// The generated <see cref="Galaxy"/>.
        /// </returns>
        Galaxy GenerateUniform(int width, int height, int numPlanets);
    }
}