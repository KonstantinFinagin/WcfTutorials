namespace GeoLib.WebHost
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Activation;

    public class CustomHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            ServiceHost host = new ServiceHost(serviceType, baseAddresses);
            return host;
            
        }
    }
}
