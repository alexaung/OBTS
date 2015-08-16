using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models.DTO
{
    public class OperatorsAgentDTO
    {
        public Guid OperatorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mobile { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Company { get; set; }

        public string CompanyPhone { get; set; }

        public string Address { get; set; }

        public Guid CountryId { get; set; }

        public string CountryName { get; set; }

        public Guid RegionId { get; set; }

        public string RegionName { get; set; }

        public Guid CityId { get; set; }

        public string CityName { get; set; }

        public int NumberOfBuses { get; set; }

        public int NumberOfRoutes { get; set; }

        public bool Status { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public Guid OperatorAgentId { get; set; }

        public Guid AgentId { get; set; }

        public string AgentName { get; set; }

        public decimal DepositAmt { get; set; }

        public DateTime JointDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}