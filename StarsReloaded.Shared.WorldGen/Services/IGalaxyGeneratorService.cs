namespace StarsReloaded.Shared.WorldGen.Services
{
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen.Meta;

    public interface IGalaxyGeneratorService
    {
        Galaxy GenerateUniform(GalaxySize size, GalaxyDensity density);

        Galaxy GenerateUniform(int width, int height, int numPlanets);
    }
}