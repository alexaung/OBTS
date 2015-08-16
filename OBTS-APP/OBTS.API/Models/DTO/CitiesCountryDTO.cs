using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models.DTO
{
    public class CitiesCountryDTO
    {
        public Guid CityId { get; set; }
        public string CityDesc { get; set; }
        public Guid RegionId { get; set; }
        public string RegionDesc { get; set; }
    }
}