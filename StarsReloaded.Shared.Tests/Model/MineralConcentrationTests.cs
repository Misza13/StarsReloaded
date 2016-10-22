namespace StarsReloaded.Shared.Tests.Model
{
    using System;

    using NUnit.Framework;

    using StarsReloaded.Shared.Model;

    [TestFixture]
    public class MineralConcentrationTests
    {
        private MineralConcentration mineralConcentration;

        [Test]
        public void Constructor_ShouldInitializePropertyFromParameter(
            [Values(0, 13, 60, 100)] int concentration)
        {
            ////Arrange & Act
            this.mineralConcentration = new MineralConcentration(concentration);

            ////Assert
            Assert.That(this.mineralConcentration.Concentration, Is.EqualTo(concentration));
        }

        [Test]
        public void Constructor_ShouldThrowWhenInvalidConcentrationIsPassed(
            [Values(-100, -1, 101, 200)] int concentration)
        {
            ////Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new MineralConcentration(concentration));
        }
    }
}