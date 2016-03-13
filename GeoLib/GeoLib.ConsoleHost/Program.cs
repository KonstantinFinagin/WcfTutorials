namespace GeoLib.ConsoleHost
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using GeoLib.Services;

    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostGeoManager = 
                new ServiceHost(
                    typeof(GeoManager), 
                    new Uri("http://localhost:8080"),
                    new Uri("net.tcp://localhost:8009"));

            ServiceMetadataBehavior behavior = hostGeoManager.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (behavior == null)
            {
                behavior = new ServiceMetadataBehavior();
                behavior.HttpGetEnabled = true;
                hostGeoManager.Description.Behaviors.Add(behavior);
            }

            hostGeoManager.AddServiceEndpoint(
                typeof (IMetadataExchange),
                MetadataExchangeBindings.CreateMexTcpBinding(),
                "MEX"
                );

            hostGeoManager.Open();

            Console.WriteLine("Services started. Press [Enter] to soutdown.");
            Console.ReadLine();

            hostGeoManager.Close();
        }
    }
}
