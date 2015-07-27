using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTSAPI.Models.DTO
{
    public class OperatorAgentDTO
    {
        public Guid OperatorAgentId { get; set; }

        public Guid OperatorId { get; set; }

        public string OperatorName { get; set; }

        public Guid AgentId { get; set; }

        public string AgentName { get; set; }

        public decimal DepositAmt { get; set; }

        public DateTime JointDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}