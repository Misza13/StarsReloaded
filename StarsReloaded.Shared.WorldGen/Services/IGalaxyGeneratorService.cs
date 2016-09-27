namespace StarsReloaded.Shared.WorldGen.Services
{
    using StarsReloaded.Shared.Model;

    public interface IGalaxyGeneratorService
    {
        Galaxy GenerateUniform(int width, int height, int numPlanets);
    }
}