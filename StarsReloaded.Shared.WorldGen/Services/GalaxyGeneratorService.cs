namespace StarsReloaded.Shared.WorldGen.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen.Helpers;
    using StarsReloaded.Shared.WorldGen.Meta;

    public class GalaxyGeneratorService : IGalaxyGeneratorService
    {
        private const int GalaxyMargin = 8;
        private const int PlanetMinDistance = 9;
        private const int MaxClusterSize = 5;
        private const int MaxFailedFitsUniform = 10;
        private const int MaxFailedFitsClump = 20;

        private readonly Random rng;

        public GalaxyGeneratorService()
        {
            this.rng = new Random();
        }

        public Galaxy Generate(GalaxySize size, GalaxyDensity density, PlanetDistribution planetDistribution)
        {
            var edge = size.GetAttributeOfType<GalaxyEdgeAttribute>().Edge;
            var num = density.GetAttributeOfType<BasePlanetCountAttribute>().Num;
            num = num * edge * edge / 160000;

            // TODO: Many of these can randomly fail, even though the chance is low There should be a global catch/retry mechanism.
            switch (planetDistribution)
            {
                case PlanetDistribution.Uniform:
                    return this.GenerateUniformInternal(edge, edge, num);
                case PlanetDistribution.FreeClumping:
                    return this.GenerateFreeClumpingInternal(edge, edge, num);
                case PlanetDistribution.UniformClumping:
                    return this.GenerateUniformClumpingInternal(edge, edge, num);
                default:
                    // We really shouldn't get here // TODO: maybe throw an exception?
                    return new Galaxy(edge, edge);
            }
        }

        private static bool PlanetFits(Galaxy galaxy, int x, int y, int minDistance)
        {
            if (x < GalaxyMargin ||
                x > galaxy.Width - GalaxyMargin ||
                y < GalaxyMargin ||
                y > galaxy.Height - GalaxyMargin)
            {
                return false;
            }

            return
                galaxy.Planets.All(
                    p =>
                        Math.Pow(x - p.X, 2) + Math.Pow(y - p.Y, 2)
                        >= minDistance * minDistance);
        }

        private Galaxy GenerateUniformInternal(int width, int height, int numPlanets)
        {
            var galaxy = new Galaxy(width, height);

            while (galaxy.Planets.Count < numPlanets)
            {
                if (galaxy.Planets.Count < numPlanets * 5 / 6)
                {
                    // First 5/6th: completely random
                    galaxy.Planets.Add(this.GeneratePlanetUniform(galaxy));
                }
                else
                {
                    // Remaining 1/6th: fill gaps
                    galaxy.Planets.Add(this.GeneratePlanetFillSpace(galaxy));
                }
            }

            return galaxy;
        }

        private Galaxy GenerateFreeClumpingInternal(int width, int height, int numPlanets)
        {
            var galaxy = new Galaxy(width, height);

            while (galaxy.Planets.Count < numPlanets)
            {
                if (galaxy.Planets.Count < numPlanets / 6)
                {
                    // First 1/6th: completely random
                    galaxy.Planets.Add(this.GeneratePlanetUniform(galaxy));
                }
                else if (galaxy.Planets.Count < numPlanets * 2 / 6)
                {
                    // Next 1/6th: fill gaps
                    galaxy.Planets.Add(this.GeneratePlanetFillSpace(galaxy));
                }
                else if (galaxy.Planets.Count < numPlanets * 6 / 6)
                {
                    // Next 1/2: clump with existing
                    galaxy.Planets.Add(this.GeneratePlanetClumped(galaxy));
                }
                else
                {
                    // Remaining 1/6th: fill gaps
                    galaxy.Planets.Add(this.GeneratePlanetFillSpace(galaxy));
                }
            }

            return galaxy;
        }

        private Galaxy GenerateUniformClumpingInternal(int width, int height, int numPlanets)
        {
            var galaxy = new Galaxy(width, height);

            while (galaxy.Planets.Count < numPlanets)
            {
                if (galaxy.Planets.Count < numPlanets * 5 / 6)
                {
                    // First 5/6th: clusters
                    var generated = this.GeneratePlanetFillSpace(galaxy);
                    var cluster = new List<Planet>() { generated };
                    var clusterSize = this.rng.Next(MaxClusterSize + 1);

                    try
                    {
                        while (cluster.Count < clusterSize && galaxy.Planets.Count < numPlanets)
                        {
                            generated = this.GeneratePlanetClumped(galaxy, cluster);
                            cluster.Add(generated);
                            galaxy.Planets.Add(generated);
                        }
                    }
                    catch (Exception)
                    {
                        // Close current cluster and continue
                    }
                }
                else
                {
                    // Remaining 1/6th: fill gaps
                    galaxy.Planets.Add(this.GeneratePlanetFillSpace(galaxy));
                }
            }

            return galaxy;
        }

        private Planet GeneratePlanetUniform(Galaxy galaxy)
        {
            var failedFits = 0;

            while (failedFits < MaxFailedFitsUniform)
            {
                var x = this.rng.Next(galaxy.Width);
                var y = this.rng.Next(galaxy.Height);

                if (PlanetFits(galaxy, x, y, PlanetMinDistance))
                {
                    return this.MakePlanet(x, y);
                }

                failedFits++;
            }

            throw new Exception("Too many failed attempts at fitting a planet.");
        }

        private Planet GeneratePlanetClumped(Galaxy galaxy, List<Planet> neighbours = null)
        {
            if (neighbours == null || neighbours.Count == 0)
            {
                neighbours = galaxy.Planets;
            }

            var failedFits = 0;

            while (failedFits < MaxFailedFitsClump)
            {
                var neighbour = neighbours[this.rng.Next(neighbours.Count)];
                var dist = this.rng.Next(9, 16);
                var angle = this.rng.NextDouble() * 2.0 * Math.PI;
                var x = neighbour.X + (int)(dist * Math.Sin(angle));
                var y = neighbour.Y + (int)(dist * Math.Cos(angle));

                if (PlanetFits(galaxy, x, y, PlanetMinDistance))
                {
                    return this.MakePlanet(x, y);
                }

                failedFits++;
            }

            throw new Exception("Too many failed attempts at fitting a planet.");
        }

        private Planet GeneratePlanetFillSpace(Galaxy galaxy)
        {
            var failedFits = 0;

            while (failedFits < MaxFailedFitsUniform * 10)
            {
                var x = this.rng.Next(galaxy.Width);
                var y = this.rng.Next(galaxy.Height);

                if (PlanetFits(galaxy, x, y, 40))
                {
                    return this.MakePlanet(x, y);
                }

                failedFits++;
            }

            throw new Exception("Too many failed attempts at fitting a planet.");
        }

        private Planet MakePlanet(int x, int y)
        {
            var planet = new Planet(x, y);
            planet.Name = Guid.NewGuid().ToString();
            planet.Gravity = new HabitationParameter(this.rng.Next(100));
            planet.Temperature = new HabitationParameter(this.rng.Next(100));
            planet.Radiation = new HabitationParameter(this.rng.Next(100));

            return planet;
        }
    }
}
