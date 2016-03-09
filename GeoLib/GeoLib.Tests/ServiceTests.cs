namespace GeoLib.Test
{
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using GeoLib.Contracts;
    using GeoLib.Services;
    using NUnit.Framework;

    [TestFixture]
    public class ServiceTests
    {
        // integration test
        [Test]
        public void test_zip_code_retrieval()
        {
            // host geo manager and make a call

            string address = "net.pipe://localhost/GeoService";
            Binding binding = new NetNamedPipeBinding();

            ServiceHost host = new ServiceHost(typeof(GeoManager));
            host.AddServiceEndpoint(typeof (IGeoService), binding, address);
            host.Open();

            var factory = new ChannelFactory<IGeoService>(binding, new EndpointAddress(address));
            IGeoService proxy = factory.CreateChannel();

            ZipCodeData data = proxy.GetZipInfo("07035");
            Assert.IsTrue(data.City.ToUpper() == "LINCOLN PARK");
            Assert.IsTrue(data.State == "NJ");
        }
    }
}
