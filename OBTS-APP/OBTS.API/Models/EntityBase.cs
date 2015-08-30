using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public abstract class EntityBase
    {
        [Column(TypeName = "datetime2")]
        public DateTime CreatedUtc { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ModifiedUtc { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }
    }
}