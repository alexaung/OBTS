using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTSAPI.Models
{
    public class OperatorAgent
    {
        public Guid OperatorAgentId { get; set; }

        public Guid OperatorId { get; set; }

        public Guid AgentId { get; set; }

        public decimal DepositAmt { get; set; }

        public DateTime JointDate { get; set; }

        public DateTime CreatedDate { get; set; }

        //navi properties
        public Operator _operator { get; set; }
        public Agent _agent { get; set; }
    }
}