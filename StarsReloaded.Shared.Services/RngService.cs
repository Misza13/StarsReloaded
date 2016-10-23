namespace StarsReloaded.Shared.Services
{
    using System;

    using Troschuetz.Random;
    using Troschuetz.Random.Distributions.Continuous;

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

        public int HabitiationParameter()
        {
            return this.rng.Next(-50, 51);
        }

        public int MineralConcentration(int highConcBias = 0)
        {
            var lowChance = 30 - (highConcBias * 5);

            if (this.Next(100) < lowChance)
            {
                return this.Next(1, 31);
            }

            return (int)TriangularDistribution.Sample(this.rng, 31, 75, 120);
        }
    }
}