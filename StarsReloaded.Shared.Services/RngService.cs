namespace StarsReloaded.Shared.Services
{
    using Troschuetz.Random;

    public class RngService : IRngService
    {
        private readonly IGenerator rng;

        public RngService()
        {
            this.rng = new TRandom();
        }

        public int Next(int maxValue)
        {
            return this.rng.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return this.rng.Next(minValue, maxValue);
        }

        public double NextDouble()
        {
            return this.rng.NextDouble();
        }
    }
}