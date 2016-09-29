namespace StarsReloaded.Shared.WorldGen.Tests
{
    using System;
    using NUnit.Framework;
    using StarsReloaded.Shared.WorldGen.Helpers;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    [TestFixture]
    public class GalaxyGeneratorServiceTests
    {
        ////System Under Test
        private IGalaxyGeneratorService galaxyGeneratorService;

        [SetUp]
        public void Setup()
        {
            this.galaxyGeneratorService = new GalaxyGeneratorService();
        }

        [Test]
        [Combinatorial]
        [Repeat(10)]
        public void GenerateFromEnumsShouldCreateCorrectGalaxy(
            [Values(GalaxySize.Tiny, GalaxySize.Small, GalaxySize.Medium, GalaxySize.Large, GalaxySize.Huge)] GalaxySize size,
            [Values(GalaxyDensity.Sparse, GalaxyDensity.Normal, GalaxyDensity.Dense, GalaxyDensity.Packed)] GalaxyDensity density,
            [Values(PlanetDistribution.Uniform, PlanetDistribution.FreeClumping, PlanetDistribution.UniformClumping)] PlanetDistribution distribution)
        {
            ////Arrange
            var edge = size.GetAttributeOfType<GalaxyEdgeAttribute>().Edge;
            var num = density.GetAttributeOfType<BasePlanetCountAttribute>().Num * edge * edge / 160000;

            ////Act
            var galaxy = this.galaxyGeneratorService.Generate(size, density, distribution);

            ////Assert
            Assert.That(galaxy.Width, Is.EqualTo(edge));
            Assert.That(galaxy.Height, Is.EqualTo(edge));
            Assert.That(galaxy.Planets.Count, Is.EqualTo(num));
            foreach (var planet in galaxy.Planets)
            {
                Assert.That(planet.X, Is.InRange(0, edge));
                Assert.That(planet.Y, Is.InRange(0, edge));
            }
        }
    }
}
