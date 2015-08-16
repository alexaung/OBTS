using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public class Agent
    {
        public Guid AgentId { get; set; }

        public string Comapany { get; set; }

        public string UserName { get; set; }

        public Guid AccountId { get; set; }

        public decimal BalanceCredit { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PanNumber { get; set; }

        public string Address { get; set; }

        public Guid CityId { get; set; }

        //public Guid RegionId { get; set; }

        public string PinCode { get; set; }

        //public Guid CountryId { get; set; }

        public string Mobile { get; set; }

        public string OfficePhone { get; set; }

        public string Fax { get; set; }

        public string UserName2 { get; set; }

        public string Password { get; set; }

        public string Logo { get; set; }

        public bool Status { get; set; }

        //navi properties
        //public Country _country { get; set; }
        //public Region _region { get; set; }
        public City _city { get; set; }
    }
}