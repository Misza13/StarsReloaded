namespace StarsReloaded.Shared.Tests.WorldGen.Services
{
    using NUnit.Framework;
    using Shouldly;
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
        [Repeat(1000)]
        public void PlanetStatsShouldFitConstraints()
        {
            ////Arrange
            var planet = new Planet(0, 0);

            ////Act
            this.planetGeneratorService.PopulatePlanetStats(planet);

            ////Assert
            planet.Name.ShouldNotBeEmpty();

            planet.Gravity.Clicks.ShouldBeInRange(-50, 50);
            planet.Temperature.Clicks.ShouldBeInRange(-50, 50);
            planet.Radiation.Clicks.ShouldBeInRange(-50, 50);

            ////TODO: Original = Current
            planet.OriginalGravity.Clicks.ShouldBeInRange(-50, 50);
            planet.OriginalTemperature.Clicks.ShouldBeInRange(-50, 50);
            planet.OriginalRadiation.Clicks.ShouldBeInRange(-50, 50);

            planet.IroniumConcentration.Concentration.ShouldBeInRange(1, 120);
            planet.BoraniumConcentration.Concentration.ShouldBeInRange(1, 120);
            planet.GermaniumConcentration.Concentration.ShouldBeInRange(1, 120);

            ////TODO: Surface minerals = 0
        }
    }
}