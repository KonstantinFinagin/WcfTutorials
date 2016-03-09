namespace GeoLib.WindowsHost
{
    using System;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Threading;
    using System.Windows;
    using GeoLib.Services;
    using GeoLib.WindowsHost.Contracts;
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

            this.syncContext = SynchronizationContext.Current;
        }

        private ServiceHost hostGeoManager = null;
        private ServiceHost hostMessageManager = null;

        private SynchronizationContext syncContext = null;

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
            this.hostMessageManager.Close();

            this.btnStart.IsEnabled = true;
            this.btnStop.IsEnabled = false;

        }

        public void ShowMessage(string message)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            SendOrPostCallback callback = arg =>
            {
                this.lblMessage.Content = message + Environment.NewLine +
                                          " (marshalled from thread " + threadId + " to thread " +
                                          Thread.CurrentThread.ManagedThreadId.ToString() +
                                          " | Process " + Process.GetCurrentProcess().Id.ToString() + ")";
                
            };
        
            this.syncContext.Send(callback, null);
        }

        private void BtnInProc_OnClick(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                // empty name to avoid a bug
                var channelFactory = new ChannelFactory<IMessageService>("");

                var proxy = channelFactory.CreateChannel();

                proxy.ShowMessage(DateTime.Now.ToLongTimeString() +
                                  " from in-process call");

                channelFactory.Close();
            });

            thread.IsBackground = true;
            thread.Start();
        }
    }
}
