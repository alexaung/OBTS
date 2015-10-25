using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models.DTO
{
    public class RouteDTO
    {
        public Guid RouteId { get; set; }

        public Guid BusId { get; set; }

        public string Company { get; set; }

        public short Brand { get; set; }

        public string BrandDesc { get; set; }

        public short BusType { get; set; }

        public string BusTypeDesc { get; set; }

        public string RegistrationNo { get; set; }

        public string VechiclePhoneNo { get; set; }

        public Guid Source_CityId { get; set; }

        public string SourceCity { get; set; }

        public Guid Destination_CityId { get; set; }

        public string DestinationCity { get; set; }

        public bool Recurrsive { get; set; }

        public DateTime RouteDate { get; set; }

        public TimeSpan DepartureTime { get; set; }

        public TimeSpan ArrivalTime { get; set; }

        public decimal RouteFare { get; set; }
    }
}