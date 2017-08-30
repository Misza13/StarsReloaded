namespace StarsReloaded.Client.View.Tests.Converters
{
    using System.Globalization;
    using System.Windows;

    using NUnit.Framework;

    using Shouldly;

    using StarsReloaded.Client.View.Converters;
    using StarsReloaded.Shared.Model;

    [TestFixture]
    public class NotNullToVisibleConverterTests
    {
        [Test]
        public void ConvertShouldMakeNullHidden()
        {
            ////Arrange
            var converter = new NotNullToVisibleConverter();

            ////Act
            var visibility = converter.Convert(null, null, null, CultureInfo.InvariantCulture);

            ////Assert
            visibility.ShouldBe(Visibility.Hidden);
        }

        [TestCase(13)]
        [TestCase("test")]
        [TestCase(16d)]
        public void ConvertShouldMakeNotNullVisible(object value)
        {
            ////Arrange
            var converter = new NotNullToVisibleConverter();

            ////Act
            var visibility = converter.Convert(value, null, null, CultureInfo.InvariantCulture);

            ////Assert
            visibility.ShouldBe(Visibility.Visible);
        }
    }
}