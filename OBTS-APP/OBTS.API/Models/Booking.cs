using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public class Booking : EntityBase
    {
        public Guid BookingId { get; set; }

        public Guid RouteId { get; set; }

        public DateTime BookingOn { get; set; }

        public string MainContact { get; set; }

        public string Email { get; set; }

        public string ContactNo { get; set; }

        public Guid DepartureCity { get; set; }

        public Guid ArrivalCity { get; set; }

        public DateTime TravelDate { get; set; }

        public decimal TotalAmt { get; set; }

        public string RegNo { get; set; }

        public string Cupon { get; set; }

        public decimal Discount { get; set; }

    }
}