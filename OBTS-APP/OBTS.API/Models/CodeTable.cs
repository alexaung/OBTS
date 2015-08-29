using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public class CodeTable : EntityBase
    {
        public Guid CodeTableId { get; set; }

        public string Title { get; set; }

        public short KeyCode { get; set; }

        public string Value { get; set; }
    }
}