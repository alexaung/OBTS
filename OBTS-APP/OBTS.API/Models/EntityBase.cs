using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public abstract class EntityBase
    {
        public DateTime CreatedUtc { get; set; }

        public DateTime ModifiedUtc { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }
    }
}