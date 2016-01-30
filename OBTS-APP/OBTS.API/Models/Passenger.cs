using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public class Passenger
    {
        public Guid PassengerId { get; set; }
        public Guid ContactDetailId { get; set; }
        public string FullName { get; set; }
        public short Age { get; set; }
        public short Gender { get; set; }
        public short IDType { get; set; }
        public string IDNumber { get; set; }
        public bool isPrimaryContact { get; set; }
        public short SeatNo { get; set; }
    }
}