namespace GeoLib.ConsoleHost
{
    using System;
    using System.ServiceModel;
    using GeoLib.Services;

    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager));
            hostGeoManager.Open();

            Console.WriteLine("Services started. Press [Enter] to soutdown.");
            Console.ReadLine();

            hostGeoManager.Close();
        }
    }
}
