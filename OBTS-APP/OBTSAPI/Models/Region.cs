using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OBTSAPI.Models
{
    public class Region
    {
        public int RegionId { get; set; }
        [Required]
        [StringLength(50)]
        public string RegionDesc { get; set; }
        [Required]
        public int CountryId { get; set; }

        //navi property
        public Country Country { get; set; }
    }
}