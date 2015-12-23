namespace GeoLib.Client.ViewModels
{
    using System.Diagnostics;
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
        }

        public string ZipCode { get; set; }
        public string State { get; set; }
        public string TextToShow { get; set; }

        public string CityText { get; set; }
        public string StateText { get; set; }


        public void GetInfo()
        {
            if (string.IsNullOrEmpty(ZipCode))
            {
                return;
            }

            GeoClient proxy = new GeoClient();

            ZipCodeData data = proxy.GetZipInfo(ZipCode);
            if (data == null)
            {
                return;
            }

            CityText = data.City;
            StateText = data.State;

            proxy.Close();
        }

        public void GetZipCodes()
        { }

        public void MakeCall()
        { }


    }
}
