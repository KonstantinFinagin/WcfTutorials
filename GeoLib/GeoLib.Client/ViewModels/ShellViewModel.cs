﻿namespace GeoLib.Client.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.Threading;
    using Caliburn.Micro;
    using GeoLib.Contracts;
    using GeoLib.Proxies;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public sealed class ShellViewModel : Screen
    {
        public ShellViewModel()
        {
            this.DisplayName = "UI running on Thread" + 
                Thread.CurrentThread.ManagedThreadId + 
                " | Process " + 
                Process.GetCurrentProcess().Id.ToString();

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

            GeoClient proxy = new GeoClient("httpEP");

            ZipCodeData data = proxy.GetZipInfo(ZipCode);
            if (data == null) return;
            
            CityText = data.City;
            StateText = data.State;

            proxy.Close();
        }

        public void GetZipCodes()
        { 
            if (string.IsNullOrEmpty(State)) return;

            EndpointAddress address = new EndpointAddress("net.tcp://localhost:8009/GeoService");
            Binding binding = new NetTcpBinding();

            GeoClient proxy = new GeoClient(binding, address);

            var data = proxy.GetZips(State);

            this.ZipCodes.Clear();

            foreach (var zipCodeData in data)
            {
                this.ZipCodes.Add(zipCodeData);
            }

            proxy.Close();
        }

        public void MakeCall()
        { }


    }
}