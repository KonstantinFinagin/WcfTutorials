namespace GeoLib.WindowsHost.Services
{
    using GeoLib.WindowsHost.Contracts;
    public class MessageManager : IMessageService
    {
        public void ShowMessage(string message)
        {
            MainWindow.MainUI.ShowMessage(message);

        }
    }
}
