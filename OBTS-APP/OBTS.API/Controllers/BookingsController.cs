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
                from r in db.Routes .Where(c=>c.RouteId==o.RouteId).DefaultIfEmpty()
                from c1 in db.Cities .Where(a=>a.CityId==o.ArrivalCity).DefaultIfEmpty()
                from c2 in db.Cities .Where(b=>b.CityId==o.DepartureCity).DefaultIfEmpty()
                            from c3 in db.CodeTables.Where(d => d.KeyCode == r._bus.BusType && d.Title == strBusType).DefaultIfEmpty()
                             select new BookingDTO()
                             {
                                BookingId=o.BookingId,
                                RouteId = o.RouteId,
                                RouteDate = r.RouteDate,
                                Company = r._bus.Company,
                                BusTypeDesc = c3.Value,
                                VechiclePhoneNo = r._bus.VechiclePhoneNo,
                                BusDescription = r._bus.Description,
                                BookingOn=o.BookingOn,
                                MainContact=o.MainContact,
                                Email=o.Email,
                                ContactNo=o.ContactNo,
                                DepartureCity=o.DepartureCity,
                                DepartureCityDesc = c2.CityDesc,
                                ArrivalCity = o.ArrivalCity,
                                ArrivalCityDesc = c1.CityDesc,
                                TravelDate=o.TravelDate,
                                TotalAmt=o.TotalAmt,
                                RegNo=o.RegNo,
                                Cupon=o.Cupon,
                                Discount=o.Discount
                             }).ToListAsync();

            return Ok(_bookings.AsQueryable());
        }

        // GET: api/bookings/bus/{BusId}/route/{RouteId}
        //default filter date : TravelDate > current date & time
        [ResponseType(typeof(BookingDTO))]
        [Route("api/bookings/bus/{BusId}/route/{RouteId}", Name = "GetBookingByBusRoute")]
        public async Task<IHttpActionResult> GetBookingByBusRoute(Guid BusId, Guid RouteId)
        {
            var bookings = await (from o in db.Bookings
                           from r in db.Routes.Where(c => c.RouteId == o.RouteId).DefaultIfEmpty()
                           from c1 in db.Cities.Where(a => a.CityId == o.ArrivalCity).DefaultIfEmpty()
                           from c2 in db.Cities.Where(b => b.CityId == o.DepartureCity).DefaultIfEmpty()
                           from c3 in db.CodeTables.Where(d => d.KeyCode == r._bus.BusType && d.Title == strBusType).DefaultIfEmpty()
                           where o.RouteId.Equals(RouteId) && r.BusId.Equals(BusId) && o.TravelDate>DateTime.Now

                           select new BookingDTO()
                         {
                             BookingId = o.BookingId,
                             RouteId = o.RouteId,
                             RouteDate = r.RouteDate,
                             Company = r._bus.Company,
                             BusTypeDesc = c3.Value,
                             VechiclePhoneNo = r._bus.VechiclePhoneNo,
                             BusDescription = r._bus.Description,
                             BookingOn = o.BookingOn,
                             MainContact = o.MainContact,
                             Email = o.Email,
                             ContactNo = o.ContactNo,
                             DepartureCity = o.DepartureCity,
                             DepartureCityDesc = c2.CityDesc,
                             ArrivalCity = o.ArrivalCity,
                             ArrivalCityDesc = c1.CityDesc,
                             TravelDate = o.TravelDate,
                             TotalAmt = o.TotalAmt,
                             RegNo = o.RegNo,
                             Cupon = o.Cupon,
                             Discount = o.Discount
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