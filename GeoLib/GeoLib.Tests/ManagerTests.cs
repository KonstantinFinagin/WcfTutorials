namespace GeoLib.Tests
{
    using GeoLib.Contracts;
    using GeoLib.Data;
    using GeoLib.Services;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class ManagerTests
    {
        [SetUp]
        public void Init()
        { }

        [Test]
        public void TestZipCodeRetrieval()
        {
            Mock<IZipCodeRepository> mockZipCodeRepository = new Mock<IZipCodeRepository>();
            ZipCode zipCode = new ZipCode()
            {
                City = "LINCOLN PARK",
                State = new State() { Abbreviation = "NJ"},
                Zip = "07035"
            };

            mockZipCodeRepository.Setup(i => i.GetByZip("07035")).Returns(zipCode);

            IGeoService geoService = new GeoManager(mockZipCodeRepository.Object);
            var data = geoService.GetZipInfo("07035");

            Assert.IsTrue(data.City.ToUpper() == "LINCOLN PARK");
            Assert.IsTrue(data.State == "NJ");
        }
    }
}
