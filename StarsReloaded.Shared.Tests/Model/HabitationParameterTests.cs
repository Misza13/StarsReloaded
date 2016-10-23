namespace StarsReloaded.Shared.Tests.Model
{
    using System;

    using NUnit.Framework;

    using StarsReloaded.Shared.Model;

    public class HabitationParameterTests
    {
        private HabitationParameter habitationParameter;

        [Test]
        public void ConstructorShouldInitializePropertyFromParameter(
            [Values(-50, -27, 12, 50)] int clicks)
        {
            ////Arrange & Act
            this.habitationParameter = new HabitationParameter(clicks);

            ////Assert
            Assert.That(this.habitationParameter.Clicks, Is.EqualTo(clicks));
        }

        [Test]
        public void ConstructorShouldThrowWhenInvalidConcentrationIsPassed(
            [Values(-100, -51, 51, 100)] int clicks)
        {
            ////Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new HabitationParameter(clicks));
        }

        [Test]
        public void IsExtremeShouldBeFalseForIntermediateValues(
            [Values(-40, 0, 40)] int clicks)
        {
            ////Arrange
            var hab = new HabitationParameter(clicks);

            ////Act & Assert
            Assert.That(hab.IsExtreme, Is.False);
        }

        [Test]
        public void IsExtremeShouldBeTrueForExtremeValues(
            [Values(-50, -41, 41, 50)] int clicks)
        {
            ////Arrange
            var hab = new HabitationParameter(clicks);

            ////Act & Assert
            Assert.That(hab.IsExtreme, Is.True);
        }
    }
}