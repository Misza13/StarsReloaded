namespace StarsReloaded.Shared.WorldGen.Tests
{
    using NUnit.Framework;
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
    }
}
