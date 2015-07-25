using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTSAPI.Models
{
    public class CountryRegionDTO
    {
        public int RegionId { get; set; }
        public string RegionDesc { get; set; }
        public int CountryId { get; set; }
        public string CountryDesc { get; set; }
    }
}