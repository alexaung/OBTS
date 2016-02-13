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

namespace OBTS.API.Controllers
{
    public class BookingPassengersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BookingPassengers
        public IQueryable<BookingPassenger> GetBookingPassengers()
        {
            return db.BookingPassengers;
        }

        // GET: api/BookingPassengers/5
        [ResponseType(typeof(BookingPassenger))]
        public async Task<IHttpActionResult> GetBookingPassenger(Guid id)
        {
            BookingPassenger bookingPassenger = await db.BookingPassengers.FindAsync(id);
            if (bookingPassenger == null)
            {
                return NotFound();
            }

            return Ok(bookingPassenger);
        }

        // PUT: api/BookingPassengers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBookingPassenger(Guid id, BookingPassenger bookingPassenger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookingPassenger.BookingPassengerId)
            {
                return BadRequest();
            }

            db.Entry(bookingPassenger).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingPassengerExists(id))
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

        // POST: api/BookingPassengers
        [ResponseType(typeof(BookingPassenger))]
        public async Task<IHttpActionResult> PostBookingPassenger(BookingPassenger bookingPassenger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BookingPassengers.Add(bookingPassenger);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookingPassengerExists(bookingPassenger.BookingPassengerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bookingPassenger.BookingPassengerId }, bookingPassenger);
        }

        // DELETE: api/BookingPassengers/5
        [ResponseType(typeof(BookingPassenger))]
        public async Task<IHttpActionResult> DeleteBookingPassenger(Guid id)
        {
            BookingPassenger bookingPassenger = await db.BookingPassengers.FindAsync(id);
            if (bookingPassenger == null)
            {
                return NotFound();
            }

            db.BookingPassengers.Remove(bookingPassenger);
            await db.SaveChangesAsync();

            return Ok(bookingPassenger);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookingPassengerExists(Guid id)
        {
            return db.BookingPassengers.Count(e => e.BookingPassengerId == id) > 0;
        }
    }
}