namespace StarsReloaded.Shared.WorldGen.Tests
{
    using NUnit.Framework;
    using StarsReloaded.Shared.WorldGen.Helpers;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    [TestFixture]
    public class GalaxyGeneratorServiceTests
    {
        ////System Under Test
        private IGalaxyGeneratorService _galaxyGeneratorService;

        [SetUp]
        public void Setup()
        {
            _galaxyGeneratorService = new GalaxyGeneratorService();
        }

        [Test]
        [Combinatorial]
        public void GenerateUniformEnums_ShouldCreateCorrectGalaxy(
            [Values(GalaxySize.Tiny, GalaxySize.Small, GalaxySize.Medium, GalaxySize.Large, GalaxySize.Huge)] GalaxySize size,
            [Values(GalaxyDensity.Sparse, GalaxyDensity.Normal, GalaxyDensity.Dense, GalaxyDensity.Packed)] GalaxyDensity density)
        {
            ////Arrange
            var edge = size.GetAttributeOfType<GalaxyEdgeAttribute>().Edge;
            var num = density.GetAttributeOfType<BasePlanetCountAttribute>().Num * edge * edge / 160000;

            ////Act
            var galaxy = _galaxyGeneratorService.GenerateUniform(size, density);

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
