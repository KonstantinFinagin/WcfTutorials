namespace GeoLib.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Threading;
    using GeoLib.Contracts;
    using GeoLib.Data;

    public class GeoManager : IGeoService
    {
        private readonly IStateRepository _StateRepository;
        private readonly IZipCodeRepository _ZipCodeRepository;

        public GeoManager()
        {
        }

        public GeoManager(IZipCodeRepository zipCodeRepository)
        {
            this._ZipCodeRepository = zipCodeRepository;
        }

        public GeoManager(IStateRepository stateRepository)
        {
            this._StateRepository = stateRepository;
        }

        public GeoManager(IZipCodeRepository zipCodeRepository, IStateRepository stateRepository)
        {
            this._ZipCodeRepository = zipCodeRepository;
            this._StateRepository = stateRepository;
        }

        public ZipCodeData GetZipInfo(string zip)
        {
            throw new DivideByZeroException("you can't do that");

            ZipCodeData zipCodeData = null;

            IZipCodeRepository zipCodeRepository = this._ZipCodeRepository ?? new ZipCodeRepository();

            ZipCode zipCodeEntity = zipCodeRepository.GetByZip(zip);

            if (zipCodeEntity != null)
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
            List<string> stateData = new List<string>();

            IStateRepository stateRepository = this._StateRepository ?? new StateRepository();

            IEnumerable<State> states = stateRepository.Get(primaryOnly);

            if (states != null)
            {
                stateData.AddRange(states.Select(state => state.Abbreviation));
            }
            
            return stateData;
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();

            IZipCodeRepository zipCodeRepository = this._ZipCodeRepository ?? new ZipCodeRepository();

            var zips = zipCodeRepository.GetByState(state);
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
            
            return zipCodeData;
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();

            IZipCodeRepository zipCodeRepository = this._ZipCodeRepository ?? new ZipCodeRepository();

            ZipCode zipEntity = zipCodeRepository.GetByZip(zip);
            IEnumerable<ZipCode> zips = zipCodeRepository.GetZipsForRange(zipEntity, range);

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
            
            return zipCodeData;
        }
    }
}
