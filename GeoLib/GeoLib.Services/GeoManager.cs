namespace GeoLib.Services
{
    using System.Collections.Generic;
    using GeoLib.Contracts;
    using GeoLib.Data;

    public class GeoManager : IGeoService
    {
        public ZipCodeData GetZipInfo(string zip)
        {
            ZipCodeData zipCodeData = null;

            IZipCodeRepository zipCodeRepository = new ZipCodeRepository();

            ZipCode zipCodeEntity = zipCodeRepository.GetByZip(zip);

            if (zipCodeData != null)
            {
                zipCodeData = new ZipCodeData()
                {
                    City = zipCodeEntity.City,
                    State = zipCodeEntity.State.Abbreviation,
                    ZipCode = zipCodeEntity.Zip
                };

            }


            return zipCodeData;
        }

        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            throw new System.NotImplementedException();
        }
    }
}
