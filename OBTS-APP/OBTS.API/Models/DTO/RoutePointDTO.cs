using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models.DTO
{
    public class RoutePointDTO
    {
        public Guid RoutePointId { get; set; }

        public Guid RouteId { get; set; }

        public string BoardingPoint { get; set; }

        public string DroppingPoint { get; set; }

        public TimeSpan BoardingTime { get; set; }

        public TimeSpan DroppingTime { get; set; }
    }
}