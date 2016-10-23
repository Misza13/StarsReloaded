namespace StarsReloaded.Shared.Services.Distributions
{
    using System;

    using Troschuetz.Random;
    using Troschuetz.Random.Distributions;
    using Troschuetz.Random.Distributions.Continuous;

    public sealed class TrapezoidalDistribution : AbstractDistribution
    {
        public TrapezoidalDistribution(IGenerator generator) : base(generator)
        {
        }

        public static Func<IGenerator, double, double, double, double, double> Sample { get; set; } = (generator, min, max, minTop, maxTop) =>
        {
            var h = 2.0 / ((max - min) + (maxTop - minTop));

            var p = generator.NextDouble();

            if (p < (minTop - min) * h / 2)
            {
                return TriangularDistribution.Sample(generator, min, minTop, minTop);
            }

            if (p >= 1 - ((max - maxTop) * h / 2))
            {
                return TriangularDistribution.Sample(generator, maxTop, max, maxTop);
            }

            return ContinuousUniformDistribution.Sample(generator, minTop, maxTop);
        };
    }
}