namespace GeoLib.Client.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.Threading;
    using Caliburn.Micro;
    using GeoLib.Client.Contracts;
    using GeoLib.Contracts;
    using GeoLib.Proxies;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public sealed class ShellViewModel : Screen
    {
        public ShellViewModel()
        {
            this.ZipCodes = new ObservableCollection<ZipCodeData>();
        }

        public string ZipCode { get; set; }
        public string State { get; set; }
        public string TextToShow { get; set; }

        public string CityText { get; set; }
        public string StateText { get; set; }

        public ObservableCollection<ZipCodeData> ZipCodes { get; set; } 
        
        public void GetInfo()
        {
            if (string.IsNullOrEmpty(ZipCode)) return;

            ServiceReference1.GeoServiceClient proxy = 
                new ServiceReference1.GeoServiceClient();

            var data = proxy.GetZipInfo(ZipCode);

            if (data != null)
            {
                CityText = data.City;
                StateText = data.State;
            }

            proxy.Close();
        }

        public void GetZipCodes()
        { 
            if (string.IsNullOrEmpty(State)) return;

            GeoClient proxy = new GeoClient("tcpEP");

            var data = proxy.GetZips(State);

            this.ZipCodes.Clear();

            foreach (var zipCodeData in data)
            {
                this.ZipCodes.Add(zipCodeData);
            }

            proxy.Close();
        }

        public void MakeCall()
        {
            // name it blank! - a bug
            ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");

            // obtaiing channel from a factory
            IMessageService proxy = factory.CreateChannel();

            proxy.ShowMsg(TextToShow);

            factory.Close();
        }


    }
}
