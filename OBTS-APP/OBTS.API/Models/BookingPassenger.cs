using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public class BookingPassenger : EntityBase
    {
        public Guid BookingPassengerId { get; set; }

        public Guid BookingDetailId { get; set; }

        public string PassengerName { get; set; }
        
        public string IDType { get; set; }

        public string IDNumber { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public Guid RouteSeatId { get; set; }

    }
}