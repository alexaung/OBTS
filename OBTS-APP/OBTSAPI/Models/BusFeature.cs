using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTSAPI.Models
{
    public class BusFeature
    {
        public Guid BusFeatureId { get; set; }

        public Guid BusId { get; set; }

        public short BusFeatureCode { get; set; }
    }
}