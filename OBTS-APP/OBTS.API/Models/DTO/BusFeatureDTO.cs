using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models.DTO
{
    public class BusFeatureDTO
    {
        public Guid BusFeatureId { get; set; }

        public Guid BusId { get; set; }

        public short BusFeatureCode { get; set; }

        public string BusFeatureDesc { get; set; }
    }
}