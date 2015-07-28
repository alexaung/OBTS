using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTSAPI.Models
{
    public class RouteDetail
    {
        public Guid RouteDetailId { get; set; }

        public Guid RouteId { get; set; }

        public short AmenitiesCodeId { get; set; }
    }
}