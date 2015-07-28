using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTSAPI.Models
{
    public class Route
    {
        public Guid RouteId { get; set; }

        public Guid BusId { get; set; }

        public Guid Source { get; set; }

        public Guid Destination { get; set; }

        public bool Recurrsive { get; set; }

        public DateTime RouteDate { get; set; }

        public TimeSpan DepartureTime { get; set; }

        public TimeSpan ArrivalTime { get; set; }

        public decimal RouteFare { get; set; }

        //navi properties
        public City _citySource { get; set; }
        public City _cityDestination { get; set; }
        public Bus _bus { get; set; }
    }
}