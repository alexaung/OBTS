using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OBTSAPI.Models
{
    public class Country
    {
        public Guid CountryId { get; set; }
        [Required]
        [StringLength(50)]
        public string CountryDesc { get; set; }
        [Required]
        [StringLength(10)]
        public string CountryCode { get; set; }
        [StringLength(20)]
        public string Currency { get; set; }
        public decimal Rate { get; set; }
        [StringLength(10)]
        public string CurrencyCode { get; set; }
    }
}