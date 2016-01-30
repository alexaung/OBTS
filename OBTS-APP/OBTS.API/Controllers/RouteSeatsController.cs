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
    public class RouteSeatsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/RouteSeats
        [ResponseType(typeof(RouteSeat))]
        public async Task<IHttpActionResult> GetRouteSeats()
        {
            return Ok((await(db.RouteSeats).ToListAsync()).AsQueryable());
        }

        // GET: api/RouteSeats/5
        [ResponseType(typeof(RouteSeat))]
        public async Task<IHttpActionResult> GetRouteSeat(Guid id)
        {
            RouteSeat routeSeat = await db.RouteSeats.FindAsync(id);
            if (routeSeat == null)
            {
                return NotFound();
            }

            return Ok(routeSeat);
        }

        

        // PUT: api/RouteSeats/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRouteSeat(Guid id, RouteSeat routeSeat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != routeSeat.RouteSeatId)
            {
                return BadRequest();
            }

            db.Entry(routeSeat).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteSeatExists(id))
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

        [ResponseType(typeof(OBTSResponse))]
        [Route("api/RouteSeats/BulkInsertRouteSeats", Name = "BulkInsertRouteSeats")]
        public async Task<IHttpActionResult> BulkInsertRouteSeats(RouteSeat[] seats)
        {
            OBTSResponse rep = new OBTSResponse();
            rep.Success = true;
            rep.Message = "";

            if (seats.Length > 0)
            {
                using (var context = new ApplicationDbContext())
                {
                    // delete existing records
                    context.Database.ExecuteSqlCommand("DELETE FROM RouteSeats WHERE RouteId = {0}", seats[0].RouteId);
                }
            }

            foreach (RouteSeat seat in seats)
            {
                seat.RouteSeatId = Guid.NewGuid();
                db.RouteSeats.Add(seat);
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                rep.Fail = false;
                rep.Message = ex.Message;
            }

            return Ok(rep);
        }

        // POST: api/RouteSeats
        [ResponseType(typeof(RouteSeat))]
        public async Task<IHttpActionResult> PostRouteSeat(RouteSeat routeSeat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RouteSeats.Add(routeSeat);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RouteSeatExists(routeSeat.RouteSeatId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = routeSeat.RouteSeatId }, routeSeat);
        }

        // DELETE: api/RouteSeats/5
        [ResponseType(typeof(RouteSeat))]
        public async Task<IHttpActionResult> DeleteRouteSeat(Guid id)
        {
            RouteSeat routeSeat = await db.RouteSeats.FindAsync(id);
            if (routeSeat == null)
            {
                return NotFound();
            }

            db.RouteSeats.Remove(routeSeat);
            await db.SaveChangesAsync();

            return Ok(routeSeat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RouteSeatExists(Guid id)
        {
            return db.RouteSeats.Count(e => e.RouteSeatId == id) > 0;
        }
    }
}