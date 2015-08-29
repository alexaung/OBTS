using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public class Region : EntityBase
    {
        public Guid RegionId { get; set; }
        [Required]
        [StringLength(50)]
        public string RegionDesc { get; set; }
        [Required]
        public Guid CountryId { get; set; }

        //navi property
        public Country Country { get; set; }
    }
}