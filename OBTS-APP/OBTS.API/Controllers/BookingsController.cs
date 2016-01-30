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
                                Discount=o.Discount
                             }).ToListAsync();

            return Ok(_bookings.AsQueryable());
        }

        // GET: api/bookings/{BookingRefId}
        //default filter date : TravelDate > current date & time
        [ResponseType(typeof(BookingDTO))]
        [Route("api/bookings/{BookingRefId}", Name = "GetBookingByBookingRefId")]
        public async Task<IHttpActionResult> GetBookingByBookingRefId(string BookingRefId)
        {
            var bookings = await (from o in db.Bookings
                           from b in db.BookingDetails.Where(b1 => b1.BookingId == o.BookingId).DefaultIfEmpty()                           
                           from r in db.Routes.Where(c => c.RouteId == b.RouteId).DefaultIfEmpty()
                           from bus in db.Buses.Where(b2 => b2.BusId == r.BusId).DefaultIfEmpty()
                           from c1 in db.Cities.Where(a => a.CityId == r.Source_CityId).DefaultIfEmpty()
                           from c2 in db.Cities.Where(b => b.CityId == r.Destination_CityId).DefaultIfEmpty()
                           from c3 in db.CodeTables.Where(d => d.KeyCode == r._bus.BusType && d.Title == strBusType).DefaultIfEmpty()
                           where o.BookingRefId.Equals(BookingRefId) 

                           select new BookingDetailDTO()
                         {
                             BookingId = o.BookingId,
                             BookingRefId=o.BookingRefId,
                             RouteId = r.RouteId,
                             RouteDate = r.RouteDate,
                             Company = bus.Company,
                             BusTypeDesc = c3.Value,
                             VechiclePhoneNo = bus.VechiclePhoneNo,
                             BusDescription = bus.Description,
                             BookingOn = o.BookingOn,
                             MainContact = o.MainContact,
                             Email = o.Email,
                             ContactNo = o.ContactNo,
                             DepartureCity = r.Source_CityId,
                             DepartureCityDesc = c2.CityDesc,
                             ArrivalCity = r.Destination_CityId,
                             ArrivalCityDesc = c1.CityDesc,
                             RouteFare = b.RouteFare,
                             
                         }).ToListAsync();
            return Ok(bookings.AsQueryable());
        }

        // GET: api/bookings/{BookingRefId}/passengers
        //default filter date : TravelDate > current date & time
        [ResponseType(typeof(BookingPassengerDTO))]
        [Route("api/bookings/{BookingRefId}/passengers", Name = "GetBookingPassengersByBookingRefId")]
        public async Task<IHttpActionResult> GetBookingPassengersByBookingRefId(string BookingRefId)
        {
            var bookings = await (from o in db.Bookings
                                  from b in db.BookingPassengers.Where(b1 => b1.BookingId == o.BookingId).DefaultIfEmpty()
                                  where o.BookingRefId.Equals(BookingRefId)

                                  select new BookingPassengerDTO()
                                  {
                                      BookingPassengerId=b.BookingPassengerId,
                                      BookingId = o.BookingId,
                                      RouteId = b.RouteId,
                                      PassengerName = b.PassengerName,
                                      IDType=b.IDType,
                                      IDNumber=b.IDNumber,
                                      Age=b.Age,
                                      Gender=b.Gender
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
                                  from b in db.BookingPassengers.Where(b1 => b1.BookingId == o.BookingId).DefaultIfEmpty()
                                  where o.BookingRefId.Equals(BookingRefId) && b.RouteId.Equals(RouteId)

                                  select new BookingPassengerDTO()
                                  {
                                      BookingPassengerId = b.BookingPassengerId,
                                      BookingId = o.BookingId,
                                      RouteId = b.RouteId,
                                      PassengerName = b.PassengerName,
                                      IDType = b.IDType,
                                      IDNumber = b.IDNumber,
                                      Age = b.Age,
                                      Gender = b.Gender
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