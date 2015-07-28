using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTSAPI.Models
{
    public class RoutePoint
    {
        public Guid RoutePointId { get; set; }

        public Guid RouteId { get; set; }

        public string BoardingPoint { get; set; }

        public string DroppingPoint { get; set; }

        public TimeSpan BoardingTime { get; set; }

        public TimeSpan DroppingTime { get; set; }

        //navi properties
       // public Route _route { get; set; }
    }
}