using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models.DTO
{
    public class BookingDTO
    {
        public Guid BookingId { get; set; }

        public string BookingRefId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        //booking info
        public DateTime BookingOn { get; set; }

        public string MainContact { get; set; }

        public string Email { get; set; }

        public string ContactNo { get; set; }

        public string Cupon { get; set; }

        public decimal Discount { get; set; }
    }
}