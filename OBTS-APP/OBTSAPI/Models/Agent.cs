﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTSAPI.Models
{
    public class Agent
    {
        public Guid AgentId { get; set; }

        public string Comapnay { get; set; }

        public string UserName { get; set; }

        public Guid AccountId { get; set; }

        public decimal BalanceCredit { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PanNumber { get; set; }

        public string Address { get; set; }

        public Guid City { get; set; }

        public Guid StateRegion { get; set; }

        public string PinCode { get; set; }

        public Guid Country { get; set; }

        public string Mobile { get; set; }

        public string OfficePhone { get; set; }

        public string Fax { get; set; }

        public string UserName2 { get; set; }

        public string Password { get; set; }

        public string Logo { get; set; }

        public bool Status { get; set; }
    }
}