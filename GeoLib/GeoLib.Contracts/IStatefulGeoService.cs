namespace GeoLib.Contracts
{
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IStatefulGeoService
    {
        [OperationContract]
        void PushZip(string zip);

        [OperationContract(IsInitiating = false)]
        ZipCodeData GetZipInfo();
        
        [OperationContract(IsInitiating = false)]
        IEnumerable<ZipCodeData> GetZips(int range);
    }
}
