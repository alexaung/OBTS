using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OBTSAPI.Models
{
    public class City
    {
        public int CityId { get; set; }
        [Required]
        [StringLength(50)]
        public string CityDesc { get; set; }
        public int RegionId { get; set; }

        //navi property
        public Region CountryRegion { get; set; }
    }
}