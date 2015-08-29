﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBTS.API.Models.DTO
{
    public class BookingDTO
    {
        public Guid BookingId { get; set; }

        //Route info
        public Guid RouteId { get; set; }

        public DateTime RouteDate { get; set; }

        public string Company { get; set; }

        public string BusTypeDesc { get; set; }

        public string VechiclePhoneNo { get; set; }

        public string BusDescription { get; set; }

        //booking info
        public DateTime BookingOn { get; set; }

        public string MainContact { get; set; }

        public string Email { get; set; }

        public string ContactNo { get; set; }

        public Guid DepartureCity { get; set; }

        public string DepartureCityDesc { get; set; }

        public Guid ArrivalCity { get; set; }

        public string ArrivalCityDesc { get; set; }

        public DateTime TravelDate { get; set; }

        public decimal TotalAmt { get; set; }

        public string RegNo { get; set; }

        public string Cupon { get; set; }

        public decimal Discount { get; set; }
    }
}