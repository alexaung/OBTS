using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTSAPI.Models
{
    public class CityDTO
    {
        public int CityId { get; set; }
        public string CityDesc { get; set; }
        public int RegionId { get; set; }
        public string RegionDesc { get; set; }
        public int CountryId { get; set; }
        public string CountryDesc { get; set; }
    }
}