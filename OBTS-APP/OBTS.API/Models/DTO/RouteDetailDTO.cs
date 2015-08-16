using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models.DTO
{
    public class RouteDetailDTO
    {
        public Guid RouteDetailId { get; set; }

        public Guid RouteId { get; set; }

        public short AmenitiesCodeId { get; set; }

        public string Amenities { get; set; }
    }
}