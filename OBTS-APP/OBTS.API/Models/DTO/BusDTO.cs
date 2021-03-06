﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models.DTO
{
    public class BusDTO
    {
        public Guid BusId { get; set; }

        public string Company { get; set; }

        public short NoOfSeat { get; set; }

        public short Brand { get; set; }

        public string BrandDesc { get; set; }

        public short BusType { get; set; }

        public string BusTypeDesc { get; set; }

        public string RegistrationNo { get; set; }

        public string PermitNumber { get; set; }

        public DateTime PermitRenewDate { get; set; }

        public string InsurancePolicyNumber { get; set; }

        public string InsuranceCompany { get; set; }

        public DateTime InsuranceValidFrom { get; set; }

        public DateTime InsuranceValidTo { get; set; }

        public string VechiclePhoneNo { get; set; }

        public string DriverName { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public Guid OperatorId { get; set; }

        public string OperatorCompany { get; set; } //instead of name , showed company of operator
    }
}