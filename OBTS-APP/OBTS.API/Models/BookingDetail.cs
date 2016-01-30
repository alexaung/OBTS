using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public class BookingDetail : EntityBase
    {
        public Guid BookingDetailId { get; set; }

        public Guid BookingId { get; set; }

        public Guid RouteId { get; set; }

        public decimal RouteFare { get; set; }
        
    }
}