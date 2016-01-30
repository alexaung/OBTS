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
    public class PassengersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Passengers
        [ResponseType(typeof(Passenger))]
        public async Task<IHttpActionResult> GetPassengers()
        {
            return Ok((await(db.Passengers).ToListAsync()).AsQueryable());
        }

        // GET: api/contactdetails/{ContactDetailId}/passengers
        [Route("api/contactdetails/{ContactDetailId}/passengers", Name = "GetPassengersByContactDetailId")]
        [ResponseType(typeof(Passenger))]
        public async Task<IHttpActionResult> GetPassengersByContactDetailId(Guid ContactDetailId)
        {
            List<Passenger> passengers = await db.Passengers.Where(u => u.ContactDetailId == ContactDetailId).ToListAsync();
            
            if (passengers == null)
            {
                return NotFound();
            }

            return Ok(passengers);
        }

        // GET: api/Passengers/5
        [ResponseType(typeof(Passenger))]
        public async Task<IHttpActionResult> GetPassenger(Guid id)
        {
            Passenger passenger = await db.Passengers.FindAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }

            return Ok(passenger);
        }

        // PUT: api/Passengers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPassenger(Guid id, Passenger passenger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != passenger.PassengerId)
            {
                return BadRequest();
            }

            db.Entry(passenger).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassengerExists(id))
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

        // POST: api/Passengers
        [ResponseType(typeof(Passenger))]
        public async Task<IHttpActionResult> PostPassenger(Passenger passenger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

	passenger.PassengerId=Guid.NewGuid();	
            db.Passengers.Add(passenger);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PassengerExists(passenger.PassengerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = passenger.PassengerId }, passenger);
        }

        // DELETE: api/Passengers/5
        [ResponseType(typeof(Passenger))]
        public async Task<IHttpActionResult> DeletePassenger(Guid id)
        {
            Passenger passenger = await db.Passengers.FindAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }

            db.Passengers.Remove(passenger);
            await db.SaveChangesAsync();

            return Ok(passenger);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PassengerExists(Guid id)
        {
            return db.Passengers.Count(e => e.PassengerId == id) > 0;
        }
    }
}