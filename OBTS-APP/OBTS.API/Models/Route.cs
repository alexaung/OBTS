using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public class Route : EntityBase
    {
        public Guid RouteId { get; set; }

        public Guid BusId { get; set; }

        public Guid Source_CityId { get; set; }

        public Guid Destination_CityId { get; set; }

        public bool Recurrsive { get; set; }

        public DateTime RouteDate { get; set; }

        public TimeSpan DepartureTime { get; set; }

        public TimeSpan ArrivalTime { get; set; }

        public decimal RouteFare { get; set; }

        //navi properties
        //public City _Source_City { get; set; }
        //public City _Destination_City { get; set; }
        public Bus _bus { get; set; }
    }
}