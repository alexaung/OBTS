using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models
{
    public class RouteSeat : EntityBase
    {
        public Guid RouteSeatId { get; set; }

       // public Guid BusId { get; set; }

        public Guid RouteId { get; set; }

        public string SeatNo { get; set; }

        public bool Bookable { get; set; }

        public bool Space { get; set; }

        public bool SpecialSeat { get; set; }

        public bool Status { get; set; }

        public short UpperLower { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }

        public short State { get; set; }

        //navi property
        public Bus _bus { get; set; }
    }
}