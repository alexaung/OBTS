using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models.DTO
{
    public class ContactDetailDTO
    {
        public Guid ContactDetailId { get; set; }
        public Guid RouteId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<Passenger> Passengers { get; set; }
    }
}