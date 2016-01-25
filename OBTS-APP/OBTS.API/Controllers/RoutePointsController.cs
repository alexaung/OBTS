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
//using OBTSAPI.DbContexts;
using OBTS.API.Models;
using System.Configuration;
using OBTS.API.Models.DTO;

namespace OBTS.API.Controllers
{
    public class RoutePointsController : ApiController
    {
        private ApplicationDbContext  db = new ApplicationDbContext ();

        // GET: api/RoutePoints
        [ResponseType(typeof(RoutePoint))]
        public async Task<IHttpActionResult> GetRoutePoints()
        {
            return Ok((await(db.RoutePoints).ToListAsync()).AsQueryable());
        }

        // GET: api/route/{Id}/routepoints
        [ResponseType(typeof(RoutePointDTO))]
        [Route("api/route/{Id}/routepoints", Name = "GetRoutePointsByRoute")]
        public async Task<IHttpActionResult> GetRoutePointsByRoute(Guid Id)
        {
            var routepoints =await( from o in db.RoutePoints
                    where o.RouteId.Equals(Id)
                              select new RoutePointDTO(){

                                    RoutePointId=o.RoutePointId,
                                    RouteId=o.RouteId,
                                    BoardingPoint=o.BoardingPoint,
                                    DroppingPoint=o.DroppingPoint,
                                    BoardingTime=o.BoardingTime,
                                    DroppingTime=o.DroppingTime
                              }).ToListAsync();

            return Ok(routepoints.AsQueryable());
        }

        // GET: api/RoutePoint/5
        [ResponseType(typeof(RoutePoint))]
        [Route("api/routepoint/{Id}", Name = "GetRoutePoint")]
        public async Task<IHttpActionResult> GetRoutePoint(Guid id)
        {
            RoutePoint routePoint = await db.RoutePoints.FindAsync(id);
            if (routePoint == null)
            {
                return NotFound();
            }

            return Ok(routePoint);
        }

        // PUT: api/RoutePoints/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoutePoint(Guid id, RoutePoint routePoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != routePoint.RoutePointId)
            {
                return BadRequest();
            }

            db.Entry(routePoint).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoutePointExists(id))
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

        // POST: api/RoutePoints
        [ResponseType(typeof(RoutePoint))]
        public async Task<IHttpActionResult> PostRoutePoint(RoutePoint routePoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            routePoint.RoutePointId = Guid.NewGuid();
            db.RoutePoints.Add(routePoint);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RoutePointExists(routePoint.RoutePointId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = routePoint.RoutePointId }, routePoint);
        }

        // DELETE: api/RoutePoints/5
        [ResponseType(typeof(RoutePoint))]
        public async Task<IHttpActionResult> DeleteRoutePoint(Guid id)
        {
            RoutePoint routePoint = await db.RoutePoints.FindAsync(id);
            if (routePoint == null)
            {
                return NotFound();
            }

            db.RoutePoints.Remove(routePoint);
            await db.SaveChangesAsync();

            return Ok(routePoint);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoutePointExists(Guid id)
        {
            return db.RoutePoints.Count(e => e.RoutePointId == id) > 0;
        }
    }
}