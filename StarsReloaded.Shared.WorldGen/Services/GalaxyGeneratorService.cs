﻿namespace StarsReloaded.Shared.WorldGen.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.Services;
    using StarsReloaded.Shared.WorldGen.Helpers;
    using StarsReloaded.Shared.WorldGen.Meta;

    public class GalaxyGeneratorService : IGalaxyGeneratorService
    {
        private const int GalaxyMargin = 8;

        private const int PlanetMinDistance = 9;

        private const int MaxClusterSize = 5;

        private const int MaxFailedFitsUniform = 10;

        private const int MaxFailedFitsClump = 20;

        private readonly IRngService rngService;
        private readonly IPlanetGeneratorService planetGeneratorService;

        public GalaxyGeneratorService(IRngService rngService, IPlanetGeneratorService planetGeneratorService)
        {
            this.rngService = rngService;
            this.planetGeneratorService = planetGeneratorService;
        }

        public Galaxy Generate(GalaxySize size, GalaxyDensity density, PlanetDistribution planetDistribution)
        {
            var edge = size.GetAttributeOfType<GalaxyEdgeAttribute>().Edge;
            var num = density.GetAttributeOfType<BasePlanetCountAttribute>().Num;
            num = num * edge * edge / 160000;

            Galaxy galaxy = null;

            while (galaxy == null)
            {
                try
                {
                    galaxy = this.GenerateGalaxyWithDistribution(planetDistribution, edge, num);
                }
                catch (PlanetPlacementException)
                {
                    ////Retry
                }
            }

            foreach (var planet in galaxy.Planets)
            {
                this.planetGeneratorService.PopulatePlanetStats(planet);
            }

            return galaxy;
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

        private Galaxy GenerateGalaxyWithDistribution(PlanetDistribution planetDistribution, int edge, int num)
        {
            Galaxy galaxy;

            switch (planetDistribution)
            {
                case PlanetDistribution.Uniform:
                    galaxy = this.GenerateUniformInternal(edge, edge, num);
                    break;
                case PlanetDistribution.FreeClumping:
                    galaxy = this.GenerateFreeClumpingInternal(edge, edge, num);
                    break;
                case PlanetDistribution.UniformClumping:
                    galaxy = this.GenerateUniformClumpingInternal(edge, edge, num);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(planetDistribution), planetDistribution, null);
            }

            return galaxy;
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
                    var clusterSize = this.rngService.Next(MaxClusterSize + 1);

                    try
                    {
                        while (cluster.Count < clusterSize && galaxy.Planets.Count < numPlanets)
                        {
                            generated = this.GeneratePlanetClumped(galaxy, cluster);
                            cluster.Add(generated);
                            galaxy.Planets.Add(generated);
                        }
                    }
                    catch (PlanetPlacementException)
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
                var x = this.rngService.Next(galaxy.Width);
                var y = this.rngService.Next(galaxy.Height);

                if (PlanetFits(galaxy, x, y, PlanetMinDistance))
                {
                    return new Planet(x, y);
                }

                failedFits++;
            }

            throw new PlanetPlacementException();
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
                var neighbour = neighbours[this.rngService.Next(neighbours.Count)];
                var dist = this.rngService.Next(9, 16);
                var angle = this.rngService.NextDouble() * 2.0 * Math.PI;
                var x = neighbour.X + (int)(dist * Math.Sin(angle));
                var y = neighbour.Y + (int)(dist * Math.Cos(angle));

                if (PlanetFits(galaxy, x, y, PlanetMinDistance))
                {
                    return new Planet(x, y);
                }

                failedFits++;
            }

            throw new PlanetPlacementException();
        }

        private Planet GeneratePlanetFillSpace(Galaxy galaxy)
        {
            var failedFits = 0;

            while (failedFits < MaxFailedFitsUniform * 10)
            {
                var x = this.rngService.Next(galaxy.Width);
                var y = this.rngService.Next(galaxy.Height);

                if (PlanetFits(galaxy, x, y, 40))
                {
                    return new Planet(x, y);
                }

                failedFits++;
            }

            throw new PlanetPlacementException();
        }
    }
}
