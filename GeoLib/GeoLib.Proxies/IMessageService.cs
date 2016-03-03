namespace GeoLib.Client.Contracts
{
    using System.ServiceModel;

    [ServiceContract]
    public interface IMessageService
    {
        [OperationContract(Name = "ShowMessage")]
        void ShowMsg(string message);
    }
}
