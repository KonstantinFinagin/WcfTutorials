namespace GeoLib.WindowsHost
{
    using System;
    using System.ServiceModel;
    using System.Threading;
    using System.Windows;
    using GeoLib.Services;
    using GeoLib.WindowsHost.Services;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow MainUI { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.btnStart.IsEnabled = true;
            this.btnStop.IsEnabled = false;

            MainUI = this;

            Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId;
        }

        private ServiceHost hostGeoManager = null;
        private ServiceHost hostMessageManager = null;

        private void BtnStart_OnClick(object sender, RoutedEventArgs e)
        {
            this.hostGeoManager = new ServiceHost(typeof (GeoManager));
            this.hostMessageManager = new ServiceHost(typeof(MessageManager));

            this.hostGeoManager.Open();
            this.hostMessageManager.Open();

            this.btnStart.IsEnabled = false;
            this.btnStop.IsEnabled = true;

        }

        private void BtnStop_OnClick(object sender, RoutedEventArgs e)
        {
            this.hostGeoManager.Close();

            this.btnStart.IsEnabled = true;
            this.btnStop.IsEnabled = false;

        }

        public void ShowMessage(string message)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            this.lblMessage.Content = message + Environment.NewLine;
        }
    }
}
