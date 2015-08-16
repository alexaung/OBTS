using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public class City
    {
        public Guid CityId { get; set; }
        [Required]
        [StringLength(50)]
        public string CityDesc { get; set; }
        public Guid RegionId { get; set; }

        //navi property
        public Region CountryRegion { get; set; }
    }
}