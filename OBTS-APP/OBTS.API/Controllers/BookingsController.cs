using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using OBTS.API.Models;
using OBTS.API.Models.DTO;

namespace OBTS.API.Controllers
{
    public class BookingsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string strBusType = OBTSEnum.ToString(OBTSEnum.Types.BusType);

        // GET: api/Bookings
        [ResponseType(typeof(BookingDTO))]
        public async Task<IHttpActionResult> GetBookings()
        {
            var _bookings =await( from o in db.Bookings
                                  from u in db.Users .Where(u1=>u1.Id==o.UserId).DefaultIfEmpty()
                              select new BookingDTO()
                             {
                                BookingId=o.BookingId,
                                BookingRefId = o.BookingRefId,
                                UserId = u.Id,
                                UserName=u.UserName,
                                BookingOn=o.BookingOn,
                                MainContact=o.MainContact,
                                Email=o.Email,
                                ContactNo=o.ContactNo,
                                Cupon=o.Cupon,
                                Discount=o.Discount,
                                BookingStatus=o.BookingStatus
                             }).ToListAsync();

            return Ok(_bookings.AsQueryable());
        }

        // GET: api/booking/{BookingRefId}
        [ResponseType(typeof(BookingDTO))]
        [Route("api/booking/{BookingRefId}", Name = "GetBookingByBookingRefId")]
        public async Task<IHttpActionResult> GetBookingByBookingRefId(string BookingRefId)
        {
            var _bookings = await (from o in db.Bookings
                                   from u in db.Users.Where(u1 => u1.Id == o.UserId).DefaultIfEmpty()
                                   where o.BookingRefId.Equals(BookingRefId)

                                   select new BookingDTO()
                                   {
                                       BookingId = o.BookingId,
                                       BookingRefId = o.BookingRefId,
                                       UserId = u.Id,
                                       UserName = u.UserName,
                                       BookingOn = o.BookingOn,
                                       MainContact = o.MainContact,
                                       Email = o.Email,
                                       ContactNo = o.ContactNo,
                                       Cupon = o.Cupon,
                                       Discount = o.Discount,
                                       BookingStatus=o.BookingStatus
                                   }).ToListAsync();

            return Ok(_bookings.AsQueryable());
        }

        [ResponseType(typeof(OBTSResponse))]
        [Route("api/booking/InsertBooking", Name = "InsertBooking")]
        public async Task<IHttpActionResult> InsertBooking(BookingDTO booking)
        {
            OBTSResponse rep = new OBTSResponse();
            rep.Success = true;
            rep.Message = "";
            Booking _booking = new Booking();
            if (booking != null)
            {
                
                _booking.BookingId = Guid.NewGuid();
                _booking.BookingOn = DateTime.Now;
                _booking.BookingRefId = "ABC123"; //Generate Booking Ref ID
                _booking.BookingStatus = booking.BookingStatus;
                _booking.ContactNo = booking.ContactNo;
                _booking.Cupon = booking.Cupon;
                _booking.Discount = booking.Discount;
                _booking.Email = booking.Email;
                _booking.MainContact = booking.MainContact;
                _booking.UserId = booking.UserId;

                db.Bookings.Add(_booking);
            }

            BookingDetail bd = new BookingDetail();
            if (booking.DepartBookingDetail != null) { 
                bd = new BookingDetail();
                bd.BookingDetailId = Guid.NewGuid();
                bd.BookingId = booking.BookingId;
                bd.RouteFare = booking.DepartBookingDetail.RouteFare;
                bd.RouteId = booking.DepartBookingDetail.RouteId;
                db.BookingDetails.Add(bd);

                foreach (BookingPassengerDTO p in booking.DepartBookingDetail.bookingPassengers)
                {
                    BookingPassenger passenger = new BookingPassenger();
                    passenger.Age = p.Age;
                    passenger.BookingDetailId = bd.BookingDetailId;
                    passenger.BookingPassengerId = Guid.NewGuid();
                    passenger.Gender = p.Gender;
                    passenger.IDNumber = p.IDNumber;
                    passenger.IDType = p.IDType;
                    passenger.PassengerName = p.PassengerName;
                    passenger.RouteSeatId = p.RouteSeatId;

                    db.BookingPassengers.Add(passenger);
                }
            }

            BookingDetail bd1 = new BookingDetail();
            if (booking.ReturnBookingDetail != null)
            {
                
                bd1.BookingDetailId = Guid.NewGuid();
                bd1.BookingId = booking.BookingId;
                bd1.RouteFare = booking.ReturnBookingDetail.RouteFare;
                bd1.RouteId = booking.ReturnBookingDetail.RouteId;
                db.BookingDetails.Add(bd1);

                foreach (BookingPassengerDTO p in booking.ReturnBookingDetail.bookingPassengers)
                {
                    BookingPassenger passenger = new BookingPassenger();
                    passenger.Age = p.Age;
                    passenger.BookingDetailId = bd1.BookingDetailId;
                    passenger.BookingPassengerId = Guid.NewGuid();
                    passenger.Gender = p.Gender;
                    passenger.IDNumber = p.IDNumber;
                    passenger.IDType = p.IDType;
                    passenger.PassengerName = p.PassengerName;
                    passenger.RouteSeatId = p.RouteSeatId;

                    db.BookingPassengers.Add(passenger);
                }
            }


            try
            {
                await db.SaveChangesAsync();

                short seatstate = 0;
                if (booking.BookingStatus == (short)OBTSEnum.BookState.Confirmed)
                    seatstate = (short)OBTSEnum.SeatState.Confirmed;
                if (booking.BookingStatus == (short)OBTSEnum.BookState.UnConfirm)
                    seatstate = (short)OBTSEnum.SeatState.UnConfirm;
                if (booking.BookingStatus == (short)OBTSEnum.BookState.Cancelled)
                    seatstate = (short)OBTSEnum.SeatState.Available;

                //booking status to seat status =>booking confirmed,unconfirm,cancelled
                foreach (BookingPassengerDTO p in booking.DepartBookingDetail.bookingPassengers)
                {
                    using (var context = new ApplicationDbContext())
                    {
                        context.Database.ExecuteSqlCommand("UPDATE RouteSeats SET State={0}  WHERE RouteSeatId = {1}", seatstate, p.RouteSeatId);
                    }
                }

                foreach (BookingPassengerDTO p in booking.ReturnBookingDetail.bookingPassengers)
                {
                    using (var context = new ApplicationDbContext())
                    {
                        context.Database.ExecuteSqlCommand("UPDATE RouteSeats SET State={0}  WHERE RouteSeatId = {1}", seatstate, p.RouteSeatId);
                    }
                }
            }
            catch (Exception ex)
            {
                rep.Fail = false;
                rep.Message = ex.Message;
            }

            return Ok(rep);
        }

        // GET: api/booking/{BookingRefId}/details
        //default filter date : TravelDate > current date & time
        [ResponseType(typeof(BookingDTO))]
        [Route("api/booking/{BookingRefId}/details", Name = "GetBookingDetailsByBookingRefId")]
        public async Task<IHttpActionResult> GetBookingDetailsByBookingRefId(string BookingRefId)
        {
            var _bookings = await (from o in db.Bookings
                            from u in db.Users.Where(u1 => u1.Id == o.UserId).DefaultIfEmpty()
                            where o.BookingRefId.Equals(BookingRefId)

                            select new BookingDTO()
                            {
                                BookingId = o.BookingId,
                                BookingRefId = o.BookingRefId,
                                UserId = u.Id,
                                UserName = u.UserName,
                                BookingOn = o.BookingOn,
                                MainContact = o.MainContact,
                                Email = o.Email,
                                ContactNo = o.ContactNo,
                                Cupon = o.Cupon,
                                Discount = o.Discount,
                                BookingStatus=o.BookingStatus
                            }).ToListAsync();

            var bookingdetails = await (from o in db.Bookings
                           from b in db.BookingDetails.Where(b1 => b1.BookingId == o.BookingId).DefaultIfEmpty()                           
                           from r in db.Routes.Where(c => c.RouteId == b.RouteId).DefaultIfEmpty()
                           from bus in db.Buses.Where(b2 => b2.BusId == r.BusId).DefaultIfEmpty()
                           from c1 in db.Cities.Where(a => a.CityId == r.Source_CityId).DefaultIfEmpty()
                           from c2 in db.Cities.Where(b => b.CityId == r.Destination_CityId).DefaultIfEmpty()
                           from c3 in db.CodeTables.Where(d => d.KeyCode == r._bus.BusType && d.Title == strBusType).DefaultIfEmpty()
                           where o.BookingRefId.Equals(BookingRefId) 

                           select new BookingDetailDTO()
                         {
                             BookingDetailId = b.BookingDetailId,
                             BookingId = o.BookingId,
                             BookingRefId=o.BookingRefId,
                             RouteId = r.RouteId,
                             RouteDate = r.RouteDate,
                             Company = bus.Company,
                             BusTypeDesc = c3.Value,
                             VechiclePhoneNo = bus.VechiclePhoneNo,
                             BusDescription = bus.Description,
                             DepartureCity = r.Source_CityId,
                             DepartureCityDesc = c2.CityDesc,
                             ArrivalCity = r.Destination_CityId,
                             ArrivalCityDesc = c1.CityDesc,
                             RouteFare = b.RouteFare,
                             
                         }).OrderBy(u => u.BookingDetailId).ToListAsync();

            List<BookingDTO> bookingDTO = (List<BookingDTO>)_bookings;

            List<BookingDetailDTO> bookingDetailDTO = (List<BookingDetailDTO>)bookingdetails;

            
            foreach (BookingDetailDTO dd in bookingDetailDTO)
            {
                var passengers = await (from o in db.Bookings
                                        from d in db.BookingDetails.Where(d1 => d1.BookingId == o.BookingId).DefaultIfEmpty()
                                        from b in db.BookingPassengers.Where(b1 => b1.BookingDetailId == d.BookingDetailId).DefaultIfEmpty()
                                        from r in db.RouteSeats.Where(r1=>r1.RouteSeatId==b.RouteSeatId).DefaultIfEmpty()
                                        where d.BookingDetailId.Equals(dd.BookingDetailId)

                                        select new BookingPassengerDTO()
                                        {
                                            BookingPassengerId = b.BookingPassengerId,
                                            BookingDetailId = d.BookingDetailId,
                                            BookingRefId = o.BookingRefId,
                                            PassengerName = b.PassengerName,
                                            IDType = b.IDType,
                                            IDNumber = b.IDNumber,
                                            Age = b.Age,
                                            Gender = b.Gender,
                                            RouteSeatId=r.RouteSeatId,
                                            SeatNo = r.SeatNo
                                        }).ToListAsync();

                List<BookingPassengerDTO> bookingPassengerDTO = (List<BookingPassengerDTO>)passengers;
                dd.bookingPassengers = bookingPassengerDTO;
            }

            if(bookingDTO.Count>0)
            { 
                bookingDTO[0].DepartBookingDetail = bookingDetailDTO[0];
                if(bookingDetailDTO.Count>1)
                    bookingDTO[0].ReturnBookingDetail = bookingDetailDTO[1];
            }
            return Ok(bookingDTO.AsQueryable());
        }

        // GET: api/bookings/{BookingRefId}/passengers
        //default filter date : TravelDate > current date & time
        [ResponseType(typeof(BookingPassengerDTO))]
        [Route("api/bookings/{BookingDetailId}/passengers", Name = "GetBookingPassengersByBookingDetailId")]
        public async Task<IHttpActionResult> GetBookingPassengersByBookingDetailId(Guid BookingDetailId)
        {
            var bookings = await (from o in db.Bookings
                                  from d in db.BookingDetails.Where(d1 => d1.BookingId==o.BookingId).DefaultIfEmpty()
                                  from b in db.BookingPassengers.Where(b1 => b1.BookingDetailId == d.BookingDetailId).DefaultIfEmpty()
                                  from r in db.RouteSeats.Where(r1 => r1.RouteSeatId == b.RouteSeatId).DefaultIfEmpty()
                                  where d.BookingDetailId.Equals(BookingDetailId)

                                  select new BookingPassengerDTO()
                                  {
                                      BookingPassengerId=b.BookingPassengerId,
                                      BookingDetailId = d.BookingDetailId,
                                      BookingRefId=o.BookingRefId,
                                      PassengerName = b.PassengerName,
                                      IDType=b.IDType,
                                      IDNumber=b.IDNumber,
                                      Age=b.Age,
                                      Gender=b.Gender,
                                      RouteSeatId=r.RouteSeatId,
                                      SeatNo=r.SeatNo
                                  }).ToListAsync();
            return Ok(bookings.AsQueryable());
        }

        // GET: api/bookings/{BookingRefId}/passengers
        //default filter date : TravelDate > current date & time
        [ResponseType(typeof(BookingPassengerDTO))]
        [Route("api/bookings/{BookingRefId}/{RouteId}/passengers", Name = "GetBookingPassengersByRouteId")]
        public async Task<IHttpActionResult> GetBookingPassengersByRouteId(string BookingRefId,Guid RouteId)
        {
            var bookings = await (from o in db.Bookings
                                  from d in db.BookingDetails.Where(d1=>d1.BookingId == o.BookingId).DefaultIfEmpty()
                                  from b in db.BookingPassengers.Where(b1 => b1.BookingDetailId == d.BookingDetailId).DefaultIfEmpty()
                                  from r in db.RouteSeats.Where(r1 => r1.RouteSeatId == b.RouteSeatId).DefaultIfEmpty()
                                  where o.BookingRefId.Equals(BookingRefId) && d.RouteId.Equals(RouteId)

                                  select new BookingPassengerDTO()
                                  {
                                      BookingPassengerId = b.BookingPassengerId,
                                      BookingDetailId = d.BookingDetailId,
                                      BookingRefId= o.BookingRefId,
                                      PassengerName = b.PassengerName,
                                      IDType = b.IDType,
                                      IDNumber = b.IDNumber,
                                      Age = b.Age,
                                      Gender = b.Gender,
                                      RouteSeatId=r.RouteSeatId,
                                      SeatNo=r.SeatNo
                                  }).ToListAsync();
            return Ok(bookings.AsQueryable());
        }

        // GET: api/Bookings/5
        [ResponseType(typeof(Booking))]
        public async Task<IHttpActionResult> GetBooking(Guid id)
        {
            Booking booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // PUT: api/Bookings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBooking(Guid id, Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != booking.BookingId)
            {
                return BadRequest();
            }

            db.Entry(booking).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();

                ///update route seats

                short seatstate = 0;
                if (booking.BookingStatus == (short)OBTSEnum.BookState.Confirmed)
                    seatstate = (short)OBTSEnum.SeatState.Confirmed;
                if (booking.BookingStatus == (short)OBTSEnum.BookState.UnConfirm)
                    seatstate = (short)OBTSEnum.SeatState.UnConfirm;
                if (booking.BookingStatus == (short)OBTSEnum.BookState.Cancelled)
                    seatstate = (short)OBTSEnum.SeatState.Available;

                //booking status to seat status =>booking confirmed,unconfirm,cancelled
                
                using (var context = new ApplicationDbContext())
                {
                    context.Database.ExecuteSqlCommand("UPDATE RouteSeats SET[State] ={0} FROM RouteSeats "+
                    "JOIN BookingPassengers on BookingPassengers.RouteSeatId = RouteSeats.RouteSeatId "+
                    "JOIN BookingDetails on BookingDetails.BookingDetailId = BookingPassengers.BookingDetailId "+
                    "JOIN Bookings on Bookings.BookingId = BookingDetails.BookingId "+
                    "WHERE Bookings.BookingId = {1}", seatstate, booking.BookingId);
                }
                
                ////
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Bookings
        [ResponseType(typeof(Booking))]
        public async Task<IHttpActionResult> PostBooking(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            booking.BookingId = Guid.NewGuid();
            db.Bookings.Add(booking);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookingExists(booking.BookingId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = booking.BookingId }, booking);
        }

        // DELETE: api/Bookings/5
        [ResponseType(typeof(Booking))]
        public async Task<IHttpActionResult> DeleteBooking(Guid id)
        {
            Booking booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            db.Bookings.Remove(booking);
            await db.SaveChangesAsync();

            return Ok(booking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookingExists(Guid id)
        {
            return db.Bookings.Count(e => e.BookingId == id) > 0;
        }
    }
}