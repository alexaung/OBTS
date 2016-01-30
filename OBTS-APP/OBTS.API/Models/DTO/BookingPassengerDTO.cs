using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public class BookingPassengerDTO 
    {
        public Guid BookingPassengerId { get; set; }

        public Guid BookingId { get; set; }

        public Guid RouteId { get; set; }

        public string PassengerName { get; set; }

        public string IDType { get; set; }

        public string IDNumber { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }
    }
}