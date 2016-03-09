namespace GeoLib.WindowsHost.Contracts
{
    using System.ServiceModel;

    [ServiceContract]
    public interface IMessageService
    {
        [OperationContract]
        void ShowMessage(string message);
    }
}
