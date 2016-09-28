namespace StarsReloaded.Shared.WorldGen.Services
{
    using System;
    using System.Linq;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.WorldGen.Helpers;
    using StarsReloaded.Shared.WorldGen.Meta;

    public class GalaxyGeneratorService : IGalaxyGeneratorService
    {
        private readonly Random _rng;

        private const int GALAXY_MARGIN = 8;
        private const int PLANET_MIN_DISTANCE = 10;
        private const int MAX_FAILED_FITS = 10;

        public GalaxyGeneratorService()
        {
            _rng = new Random();
        }

        public Galaxy GenerateUniform(GalaxySize size, GalaxyDensity density)
        {
            var edge = size.GetAttributeOfType<GalaxyEdgeAttribute>().Edge;
            var num = density.GetAttributeOfType<BasePlanetCountAttribute>().Num;

            return GenerateUniform(edge, edge, num * edge * edge / 160000);
        }

        public Galaxy GenerateUniform(int width, int height, int numPlanets)
        {
            var galaxy = new Galaxy(width, height);

            for (var i = 0; i < numPlanets; i++)
            {
                var failedFits = 0;
                Planet candidate = null;
                while (failedFits < MAX_FAILED_FITS)
                {
                    candidate = GeneratePlanetUniform(galaxy.Width, galaxy.Height);
                    if (!PlanetFits(galaxy, candidate))
                    {
                        failedFits++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (failedFits == MAX_FAILED_FITS)
                {
                    throw new Exception("Too many failed attempts at fitting a planet.");
                }

                galaxy.Planets.Add(candidate);
            }

            return galaxy;
        }

        private static bool PlanetFits(Galaxy galaxy, Planet candidate)
        {
            if (candidate.X < GALAXY_MARGIN ||
                candidate.X > galaxy.Width - GALAXY_MARGIN ||
                candidate.Y < GALAXY_MARGIN ||
                candidate.Y > galaxy.Height - GALAXY_MARGIN)
            {
                return false;
            }

            return galaxy.Planets.All(p => Math.Pow(candidate.X - p.X, 2) + Math.Pow(candidate.Y - p.Y, 2) >=
                                            PLANET_MIN_DISTANCE*PLANET_MIN_DISTANCE);
        }

        private Planet GeneratePlanetUniform(int width, int height)
        {
            var x = _rng.Next(width);
            var y = _rng.Next(height);

            //TODO: name generation
            //TODO: hab values
            //TODO: mineral concentration
            return new Planet(x, y, Guid.NewGuid().ToString());
        }
    }
}
