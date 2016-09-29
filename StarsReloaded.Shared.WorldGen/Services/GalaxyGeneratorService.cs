namespace StarsReloaded.Shared.WorldGen.Services
{
    using System;
    using System.Linq;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen.Helpers;
    using StarsReloaded.Shared.WorldGen.Meta;

    public class GalaxyGeneratorService : IGalaxyGeneratorService
    {
        private const int GalaxyMargin = 8;
        private const int PlanetMinDistance = 10;
        private const int MaxFailedFits = 10;

        private readonly Random rng;

        public GalaxyGeneratorService()
        {
            this.rng = new Random();
        }

        public Galaxy GenerateUniform(GalaxySize size, GalaxyDensity density)
        {
            var edge = size.GetAttributeOfType<GalaxyEdgeAttribute>().Edge;
            var num = density.GetAttributeOfType<BasePlanetCountAttribute>().Num;

            return this.GenerateUniform(edge, edge, num * edge * edge / 160000);
        }

        public Galaxy GenerateUniform(int width, int height, int numPlanets)
        {
            var galaxy = new Galaxy(width, height);

            for (var i = 0; i < numPlanets; i++)
            {
                var failedFits = 0;
                Planet candidate = null;
                while (failedFits < MaxFailedFits)
                {
                    candidate = this.GeneratePlanetUniform(galaxy);
                    if (!PlanetFits(galaxy, candidate))
                    {
                        failedFits++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (failedFits == MaxFailedFits)
                {
                    throw new Exception("Too many failed attempts at fitting a planet.");
                }

                galaxy.Planets.Add(candidate);
            }

            return galaxy;
        }

        /// <summary>
        /// Evaluate whether a given candidate planet fits within the galaxy, obeying all constraints.
        /// </summary>
        /// <param name="galaxy">
        /// The containing galaxy.
        /// </param>
        /// <param name="candidate">
        /// The candidate planet.
        /// </param>
        /// <returns>
        /// Indication whether the candidate placement is valid.
        /// </returns>
        private static bool PlanetFits(Galaxy galaxy, Planet candidate)
        {
            if (candidate.X < GalaxyMargin ||
                candidate.X > galaxy.Width - GalaxyMargin ||
                candidate.Y < GalaxyMargin ||
                candidate.Y > galaxy.Height - GalaxyMargin)
            {
                return false;
            }

            return
                galaxy.Planets.All(
                    p =>
                        Math.Pow(candidate.X - p.X, 2) + Math.Pow(candidate.Y - p.Y, 2)
                        >= PlanetMinDistance * PlanetMinDistance);
        }

        /// <summary>
        /// Generate a new random planet with a uniform distribution.
        /// </summary>
        /// <param name="galaxy">
        /// The galaxy in which to put the planet.
        /// </param>
        /// <returns>
        /// The generated <see cref="Planet"/>.
        /// </returns>
        private Planet GeneratePlanetUniform(Galaxy galaxy)
        {
            var x = this.rng.Next(galaxy.Width);
            var y = this.rng.Next(galaxy.Height);

            // TODO: name generation
            // TODO: hab values
            // TODO: mineral concentration
            return new Planet(x, y, Guid.NewGuid().ToString());
        }
    }
}
