using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTSAPI.Models.DTO
{
    public class CityDTO
    {
        public Guid CityId { get; set; }
        public string CityDesc { get; set; }
        public Guid RegionId { get; set; }
        public string RegionDesc { get; set; }
        public Guid CountryId { get; set; }
        public string CountryDesc { get; set; }
    }
}