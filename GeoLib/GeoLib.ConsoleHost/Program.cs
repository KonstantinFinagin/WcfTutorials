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
            ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager));
            hostGeoManager.Open();

            ServiceDebugBehavior behavior = hostGeoManager.Description.Behaviors.Find<ServiceDebugBehavior>();
            if (behavior == null)
            {
                behavior = new ServiceDebugBehavior();
                behavior.IncludeExceptionDetailInFaults = true;
                hostGeoManager.Description.Behaviors.Add(behavior);
            }
            else
            {
                behavior.IncludeExceptionDetailInFaults = true;
            }

            Console.WriteLine("Services started. Press [Enter] to soutdown.");
            Console.ReadLine();

            hostGeoManager.Close();
        }
    }
}
