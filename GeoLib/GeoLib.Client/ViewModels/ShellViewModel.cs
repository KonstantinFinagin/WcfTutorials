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
        private StatefulGeoClient statefulProxy = null;

        public ShellViewModel()
        {
            this.ZipCodes = new ObservableCollection<ZipCodeData>();
            this.statefulProxy = new StatefulGeoClient();
        }

        public string Range { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string TextToShow { get; set; }

        public string CityText { get; set; }
        public string StateText { get; set; }

        public ObservableCollection<ZipCodeData> ZipCodes { get; set; } 
        
        public void GetInfo()
        {
            
            if (string.IsNullOrEmpty(ZipCode)) return;
            
            var data = this.statefulProxy.GetZipInfo();

            if (data != null)
            {
                CityText = data.City;
                StateText = data.State;
            }
            
        }

        public void GetZipCodes()
        {
            var tempProxy = new GeoClient("tcpEP");
            if (string.IsNullOrEmpty(State)) return;

            var data = tempProxy.GetZips(State);

            this.ZipCodes.Clear();

            foreach (var zipCodeData in data)
            {
                this.ZipCodes.Add(zipCodeData);
            }
            
            tempProxy.Close();
        }

        public void Push()
        {
            if (string.IsNullOrEmpty(ZipCode)) return;

            this.statefulProxy.PushZip(ZipCode);
        }

        public void GetInRange()
        {
            
            if (string.IsNullOrEmpty(Range)) return;

            var data = this.statefulProxy.GetZips(int.Parse(Range));
            if (data == null) return;

            this.ZipCodes.Clear();
            foreach (var zipCodeData in data)
            {
                this.ZipCodes.Add(zipCodeData);
            }
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
