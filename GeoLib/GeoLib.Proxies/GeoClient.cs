﻿namespace GeoLib.Proxies
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using GeoLib.Contracts;

    public class GeoClient : ClientBase<IGeoService>, IGeoService
    {
        public ZipCodeData GetZipInfo(string zip)
        {
            return Channel.GetZipInfo(zip);
        }

        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            return Channel.GetStates(primaryOnly);
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            return Channel.GetZips(state);
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            return Channel.GetZips(zip, range);
        }
    }
}
