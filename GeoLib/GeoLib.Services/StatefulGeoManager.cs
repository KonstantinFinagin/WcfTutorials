using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.Services
{
    using System.ServiceModel;
    using GeoLib.Contracts;
    using GeoLib.Data;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class StatefulGeoManager : IStatefulGeoService
    {
        private ZipCode zipCodeEntity;

        public void PushZip(string zip)
        {
            IZipCodeRepository zipCodeRepository = new ZipCodeRepository();

            this.zipCodeEntity = zipCodeRepository.GetByZip(zip);
        }

        public ZipCodeData GetZipInfo()
        {
            ZipCodeData zipCodeData = null;

            if (this.zipCodeEntity != null)
            {
                zipCodeData = new ZipCodeData()
                {
                    City = this.zipCodeEntity.City,
                    State = this.zipCodeEntity.State.Abbreviation,
                    ZipCode = this.zipCodeEntity.Zip
                };
            }
            else
            {
                throw new ApplicationException("Uh oh");
            }
            return zipCodeData;
        }

        public IEnumerable<ZipCodeData> GetZips(int range)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();

            if (this.zipCodeEntity != null)
            {
                IZipCodeRepository zipCodeRepository = new ZipCodeRepository();

                IEnumerable<ZipCode> zips = zipCodeRepository.GetZipsForRange(this.zipCodeEntity, range);

                if (zips != null)
                {
                    foreach (var zipCode in zips)
                    {
                        zipCodeData.Add(new ZipCodeData()
                        {
                            City = zipCode.City,
                            State = zipCode.State.Abbreviation,
                            ZipCode = zipCode.Zip
                        });
                    }
                }
            }
            else
            {
                throw new ApplicationException("Uh oh");
            }

            return zipCodeData;
        }
    }
}
