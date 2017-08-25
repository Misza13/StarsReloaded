namespace StarsReloaded.Shared.Tests.WorldGen.Services
{
    using Moq;
    using NUnit.Framework;
    using Shouldly;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.Services;
    using StarsReloaded.Shared.WorldGen.Helpers;
    using StarsReloaded.Shared.WorldGen.Meta;
    using StarsReloaded.Shared.WorldGen.Services;

    [TestFixture]
    public class GalaxyGeneratorServiceTests
    {
        ////System Under Test
        private IGalaxyGeneratorService galaxyGeneratorService;

        ////Dependencies
        private Mock<IPlanetGeneratorService> planetGeneratorService;

        [SetUp]
        public void Setup()
        {
            this.planetGeneratorService = new Mock<IPlanetGeneratorService>();
            this.planetGeneratorService.Setup(s => s.PopulatePlanetStats(It.IsAny<Planet>()));

            this.galaxyGeneratorService = new GalaxyGeneratorService(new RngService(), planetGeneratorService.Object);
        }

        [Test]
        [Combinatorial]
        public void GenerateShouldCreateCorrectGalaxy(
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
            galaxy.Width.ShouldBe(edge);
            galaxy.Height.ShouldBe(edge);
            galaxy.Planets.Count.ShouldBe(num);

            foreach (var planet in galaxy.Planets)
            {
                planet.X.ShouldBeInRange(0, edge);
                planet.Y.ShouldBeInRange(0, edge);
            }
        }
    }
}
