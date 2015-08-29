using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public class RouteDetail : EntityBase
    {
        public Guid RouteDetailId { get; set; }

        public Guid RouteId { get; set; }

        public short AmenitiesCodeId { get; set; }
    }
}