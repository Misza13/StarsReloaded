namespace StarsReloaded.Shared.Tests.WorldGen.Services
{
    using Moq;
    using NUnit.Framework;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.Services;
    using StarsReloaded.Shared.WorldGen.Services;

    public class PlanetGeneratorServiceTests
    {
        ////System Under Test
        private PlanetGeneratorService planetGeneratorService;

        [SetUp]
        public void Setup()
        {
            this.planetGeneratorService = new PlanetGeneratorService(new RngService());
        }

        [Test]
        public void PlanetStatsShouldFitConstraints()
        {
            ////Arrange
            var planet = new Planet(0, 0);

            ////Act
            this.planetGeneratorService.PopulatePlanetStats(planet);

            ////Assert
            Assert.That(planet.Name, Is.Not.Empty);

            Assert.That(planet.Gravity.Clicks, Is.InRange(-50, 50));
            Assert.That(planet.Temperature.Clicks, Is.InRange(-50, 50));
            Assert.That(planet.Radiation.Clicks, Is.InRange(-50, 50));

            ////TODO: Original = Current
            Assert.That(planet.OriginalGravity.Clicks, Is.InRange(-50, 50));
            Assert.That(planet.OriginalTemperature.Clicks, Is.InRange(-50, 50));
            Assert.That(planet.OriginalRadiation.Clicks, Is.InRange(-50, 50));

            Assert.That(planet.IroniumConcentration.Concentration, Is.InRange(1, 120));
            Assert.That(planet.BoraniumConcentration.Concentration, Is.InRange(1, 120));
            Assert.That(planet.GermaniumConcentration.Concentration, Is.InRange(1, 120));

            ////TODO: Surface minerals = 0
        }
    }
}