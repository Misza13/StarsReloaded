namespace StarsReloaded.Shared.WorldGen
{
    using System;
    using System.Linq;
    using StarsReloaded.Shared.Model;

    public class GalaxyGenerator
    {
        private readonly int _width;
        private readonly int _height;
        private readonly Random _rng;
        private Galaxy _galaxy;

        private const int GALAXY_MARGIN = 8;
        private const int PLANET_MIN_DISTANCE = 10;

        public GalaxyGenerator(int width, int height)
        {
            _width = width;
            _height = height;
            _rng = new Random();
        }

        public Galaxy GenerateUniform(int numPlanets)
        {
            _galaxy = new Galaxy();

            for (var i = 0; i < numPlanets; i++)
            {
                var fit = false;
                Planet candidate = null;
                while (!fit)
                {
                    candidate = GeneratePlanetUniform(_width, _height);
                    fit = PlanetFits(_width, _height, candidate);
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

            return new Planet(x, y);
        }
    }
}
