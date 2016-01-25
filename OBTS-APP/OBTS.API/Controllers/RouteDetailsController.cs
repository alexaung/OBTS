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
    public class RouteDetailsController : ApiController
    {
        private ApplicationDbContext  db = new ApplicationDbContext ();
        private string strAmenities = OBTSEnum.ToString(OBTSEnum.Types.Amenities);

        // GET: api/routedetails
        [ResponseType(typeof(RouteDetailDTO))]
        [Route("api/routedetails/amenities", Name = "GetRouteDetails")]
        public async Task<IHttpActionResult> GetRouteDetails()
        {
            var routedetails =await( from b in db.RouteDetails
                        join ct in db.CodeTables on b.AmenitiesCodeId equals ct.KeyCode
                               where ct.Title.Equals(strAmenities)

                               select new RouteDetailDTO()
                                {
                                    RouteDetailId=b.RouteDetailId,
                                    RouteId=b.RouteId,
                                    AmenitiesCodeId=b.AmenitiesCodeId,
                                    Amenities=ct.Value

                                }).ToListAsync();
            return Ok(routedetails.AsQueryable());
        }

        // GET: api/route/{Id}/routedetails
        [ResponseType(typeof(RouteDetailDTO))]
        [Route("api/route/{Id}/amenities", Name = "GetRouteDetailsByRoute")]
        public async Task<IHttpActionResult> GetRouteDetailsByRoute(Guid Id)
        {
            var routedetails =await( from b in db.RouteDetails
                               join ct in db.CodeTables on b.AmenitiesCodeId equals ct.KeyCode
                               where ct.Title.Equals(strAmenities) && b.RouteId.Equals(Id)

                               select new RouteDetailDTO()
                               {
                                   RouteDetailId = b.RouteDetailId,
                                   RouteId = b.RouteId,
                                   AmenitiesCodeId = b.AmenitiesCodeId,
                                   Amenities = ct.Value

                               }).ToListAsync();
            return Ok(routedetails.AsQueryable());
        }

        // GET: api/routeamenity/5
        [ResponseType(typeof(RouteDetail))]
        [Route("api/routedetail/{Id}/amenities", Name = "GetRouteDetail")]
        public async Task<IHttpActionResult> GetRouteDetail(Guid id)
        {
            var routeDetail = await( from b in db.RouteDetails
                               join ct in db.CodeTables on b.AmenitiesCodeId equals ct.KeyCode
                               where ct.Title.Equals(strAmenities) && b.RouteDetailId.Equals(id)

                               select new RouteDetailDTO()
                               {
                                   RouteDetailId = b.RouteDetailId,
                                   RouteId = b.RouteId,
                                   AmenitiesCodeId = b.AmenitiesCodeId,
                                   Amenities = ct.Value

                               }).SingleOrDefaultAsync(r => r.RouteDetailId == id);

            //RouteDetailDTO routeDetail = await routedetails;
            if (routeDetail == null)
            {
                return NotFound();
            }

            return Ok(routeDetail);
        }

        // PUT: api/RouteDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRouteDetail(Guid id, RouteDetail routeDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != routeDetail.RouteDetailId)
            {
                return BadRequest();
            }

            db.Entry(routeDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteDetailExists(id))
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

        // POST: api/RouteDetails
        [ResponseType(typeof(RouteDetail))]
        public async Task<IHttpActionResult> PostRouteDetail(RouteDetail routeDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            routeDetail.RouteDetailId = Guid.NewGuid();
            db.RouteDetails.Add(routeDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RouteDetailExists(routeDetail.RouteDetailId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = routeDetail.RouteDetailId }, routeDetail);
        }

        // DELETE: api/RouteDetails/5
        [ResponseType(typeof(RouteDetail))]
        public async Task<IHttpActionResult> DeleteRouteDetail(Guid id)
        {
            RouteDetail routeDetail = await db.RouteDetails.FindAsync(id);
            if (routeDetail == null)
            {
                return NotFound();
            }

            db.RouteDetails.Remove(routeDetail);
            await db.SaveChangesAsync();

            return Ok(routeDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RouteDetailExists(Guid id)
        {
            return db.RouteDetails.Count(e => e.RouteDetailId == id) > 0;
        }
    }
}