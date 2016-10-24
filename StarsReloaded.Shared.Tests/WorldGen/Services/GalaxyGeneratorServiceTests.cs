namespace StarsReloaded.Shared.Tests.WorldGen.Services
{
    using NUnit.Framework;

    using StarsReloaded.Shared.Services;
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
            this.galaxyGeneratorService = new GalaxyGeneratorService(new RngService());
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

                Assert.That(planet.Name, Is.Not.Empty);

                Assert.That(planet.Gravity.Clicks, Is.InRange(-50, 50));
                Assert.That(planet.Temperature.Clicks, Is.InRange(-50, 50));
                Assert.That(planet.Radiation.Clicks, Is.InRange(-50, 50));

                Assert.That(planet.OriginalGravity.Clicks, Is.InRange(-50, 50));
                Assert.That(planet.OriginalTemperature.Clicks, Is.InRange(-50, 50));
                Assert.That(planet.OriginalRadiation.Clicks, Is.InRange(-50, 50));

                Assert.That(planet.IroniumConcentration.Concentration, Is.InRange(1, 120));
                Assert.That(planet.BoraniumConcentration.Concentration, Is.InRange(1, 120));
                Assert.That(planet.GermaniumConcentration.Concentration, Is.InRange(1, 120));
            }
        }
    }
}
