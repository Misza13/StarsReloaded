namespace StarsReloaded.Shared.WorldGen
{
    using System;
    using System.Linq;
    using StarsReloaded.Shared.Model;

    public class GalaxyGenerator
    {
        private readonly Random _rng;
        private readonly Galaxy _galaxy;

        private const int GALAXY_MARGIN = 8;
        private const int PLANET_MIN_DISTANCE = 10;

        public GalaxyGenerator(int width, int height)
        {
            _rng = new Random();
            _galaxy = new Galaxy(width, height);
        }

        public Galaxy GenerateUniform(int numPlanets)
        {
            for (var i = 0; i < numPlanets; i++)
            {
                var fit = false;
                Planet candidate = null;
                while (!fit)
                {
                    candidate = GeneratePlanetUniform(_galaxy.Width, _galaxy.Height);
                    fit = PlanetFits(_galaxy.Width, _galaxy.Height, candidate);
                    ////TODO: exit strategy if looped
                }

                _galaxy.Planets.Add(candidate);
            }

            return _galaxy;
        }

        private bool PlanetFits(int width, int height, Planet candidate)
        {
            if (candidate.X < GALAXY_MARGIN ||
                candidate.X > width - GALAXY_MARGIN ||
                candidate.Y < GALAXY_MARGIN ||
                candidate.Y > height - GALAXY_MARGIN)
            {
                return false;
            }

            return _galaxy.Planets.All(p => Math.Pow(candidate.X - p.X, 2) + Math.Pow(candidate.Y - p.Y, 2) >=
                                            PLANET_MIN_DISTANCE*PLANET_MIN_DISTANCE);
        }

        private Planet GeneratePlanetUniform(int width, int height)
        {
            var x = _rng.Next(width);
            var y = _rng.Next(height);

            return new Planet(x, y, Guid.NewGuid().ToString());
        }
    }
}
