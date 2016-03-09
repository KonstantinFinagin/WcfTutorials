namespace GeoLib.WindowsHost.Services
{
    using System.ServiceModel;
    using GeoLib.WindowsHost.Contracts;

    [ServiceBehavior(UseSynchronizationContext = false)]
    public class MessageManager : IMessageService
    {
        public void ShowMessage(string message)
        {
            MainWindow.MainUI.ShowMessage(message);
        }
    }
}
