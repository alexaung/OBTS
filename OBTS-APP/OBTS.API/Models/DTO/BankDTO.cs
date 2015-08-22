using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models.DTO
{
    public class BankDTO
    {
        public Guid BankId { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string UserTypeDesc { get; set; }

        public string BankName { get; set; }

        public string Branch { get; set; }

        public string AccountNumber { get; set; }

        public string LFSCCode { get; set; }

        public string Logo { get; set; }
    }
}