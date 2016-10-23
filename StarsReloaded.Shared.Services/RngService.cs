namespace StarsReloaded.Shared.Services
{
    using System;

    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.Services.Distributions;

    using Troschuetz.Random;
    using Troschuetz.Random.Distributions.Continuous;
    using Troschuetz.Random.Generators;

    public class RngService : IRngService
    {
        private readonly IGenerator rng;

        public RngService()
        {
            this.rng = new MT19937Generator();
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

        public int HabitiationParameter(HabitationParameterType habitationParameterType)
        {
            switch (habitationParameterType)
            {
                case HabitationParameterType.Gravity:
                case HabitationParameterType.Temperature:
                    return (int)Math.Round(TrapezoidalDistribution.Sample(this.rng, -49.5, 49.5, -40, 40));
                case HabitationParameterType.Radiation:
                    return (int)Math.Round(ContinuousUniformDistribution.Sample(this.rng, -49.5, 49.5));
                default:
                    throw new ArgumentOutOfRangeException(nameof(habitationParameterType), habitationParameterType, null);
            }
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