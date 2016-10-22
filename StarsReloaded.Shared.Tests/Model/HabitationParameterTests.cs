namespace StarsReloaded.Shared.Tests.Model
{
    using System;

    using NUnit.Framework;

    using StarsReloaded.Shared.Model;

    public class HabitationParameterTests
    {
        private HabitationParameter habitationParameter;

        [Test]
        public void Constructor_ShouldInitializePropertyFromParameter(
            [Values(-50, -27, 12, 50)] int clicks)
        {
            ////Arrange & Act
            this.habitationParameter = new HabitationParameter(clicks);

            ////Assert
            Assert.That(this.habitationParameter.Clicks, Is.EqualTo(clicks));
        }

        [Test]
        public void Constructor_ShouldThrowWhenInvalidConcentrationIsPassed(
            [Values(-100, -51, 51, 100)] int clicks)
        {
            ////Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new HabitationParameter(clicks));
        }
    }
}