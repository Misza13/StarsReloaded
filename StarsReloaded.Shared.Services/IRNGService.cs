namespace StarsReloaded.Shared.Services
{
    /// <summary>
    /// Random Number Generator service is a wrapper around the RNG library.
    /// </summary>
    public interface IRngService
    {
        int Next(int maxValue);

        int Next(int minValue, int maxValue);

        double NextDouble();

        int HabitiationParameter();

        int MineralConcentration(int highConcBias = 0);
    }
}