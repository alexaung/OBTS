using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public class Bank
    {
        public Guid BankId { get; set; }

        public Guid UserId { get; set; }

        public short UserTypeCode { get; set; }

        public string BankName { get; set; }

        public string Branch { get; set; }

        public string AccountNumber { get; set; }

        public string LFSCCode { get; set; }

        public string Logo { get; set; }
    }
}