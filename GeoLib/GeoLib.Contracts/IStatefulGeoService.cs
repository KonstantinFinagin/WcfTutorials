namespace GeoLib.Contracts
{
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IStatefulGeoService
    {
        [OperationContract]
        void PushZip(string zip);

        [OperationContract]
        ZipCodeData GetZipInfo();
        
        [OperationContract]
        IEnumerable<ZipCodeData> GetZips(int range);
    }
}
