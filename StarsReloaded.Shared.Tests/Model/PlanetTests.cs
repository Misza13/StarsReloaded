namespace StarsReloaded.Shared.Tests.Model
{
    using NUnit.Framework;

    using StarsReloaded.Shared.Model;

    [TestFixture]
    public class PlanetTests
    {
        [Test]
        public void ConstructorShouldInitializeCoordsFromParameters([Values(100, 250, 333)] int x, [Values(170, 211, 580)] int y)
        {
            ////Arrange & Act
            var planet = new Planet(x, y);

            ////Assert
            Assert.That(planet.X, Is.EqualTo(x));
            Assert.That(planet.Y, Is.EqualTo(y));
        }

        [Test]
        public void GetSurfaceMineralsShouldReturnCorrectAmount()
        {
            ////Arrange
            const int SurfaceIronium = 2570;
            const int SurfaceBoranium = 3777;
            const int SurfaceGermanium = 1800;

            var planet = new Planet(1337, 1337);
            planet.SurfaceIronium = SurfaceIronium;
            planet.SurfaceBoranium = SurfaceBoranium;
            planet.SurfaceGermanium = SurfaceGermanium;

            ////Act
            var ironium = planet.GetSurfaceMinerals(MineralType.Ironium);
            var boranium = planet.GetSurfaceMinerals(MineralType.Boranium);
            var germanium = planet.GetSurfaceMinerals(MineralType.Germanium);

            ////Assert
            Assert.That(ironium, Is.EqualTo(SurfaceIronium));
            Assert.That(boranium, Is.EqualTo(SurfaceBoranium));
            Assert.That(germanium, Is.EqualTo(SurfaceGermanium));
        }
    }
}