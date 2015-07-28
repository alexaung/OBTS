﻿using System;
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
using OBTSAPI.DbContexts;
using OBTSAPI.Models;
using System.Configuration;
using OBTSAPI.Models.DTO;

namespace OBTSAPI.Controllers
{
    public class RouteDetailsController : ApiController
    {
        private OBTSDbContext db = new OBTSDbContext(ConfigurationManager.AppSettings["OBTSDb"].ToString());

        // GET: api/routedetails
        [Route("api/routesamenities", Name = "GetRouteDetails")]
        public IQueryable<RouteDetailDTO> GetRouteDetails()
        {
            var routedetails = from b in db.RouteDetails
                        join ct in db.CodeTables on b.AmenitiesCodeId equals ct.KeyCode
                        where ct.Title.Equals("Amenities")

                               select new RouteDetailDTO()
                                {
                                    RouteDetailId=b.RouteDetailId,
                                    RouteId=b.RouteId,
                                    AmenitiesCodeId=b.AmenitiesCodeId,
                                    Amenities=ct.Value

                                };
            return routedetails;
        }

        // GET: api/route/{Id}/routedetails
        [Route("api/route/{Id}/amenities", Name = "GetRouteDetailsByRoute")]
        public IQueryable<RouteDetailDTO> GetRouteDetailsByRoute(Guid Id)
        {
            var routedetails = from b in db.RouteDetails
                               join ct in db.CodeTables on b.AmenitiesCodeId equals ct.KeyCode
                               where ct.Title.Equals("Amenities") && b.RouteId.Equals(Id)

                               select new RouteDetailDTO()
                               {
                                   RouteDetailId = b.RouteDetailId,
                                   RouteId = b.RouteId,
                                   AmenitiesCodeId = b.AmenitiesCodeId,
                                   Amenities = ct.Value

                               };
            return routedetails;
        }

        // GET: api/routeamenity/5
        [ResponseType(typeof(RouteDetail))]
        [Route("api/routeamenity/{Id}", Name = "GetRouteDetail")]
        public async Task<IHttpActionResult> GetRouteDetail(Guid id)
        {
            var routedetails = from b in db.RouteDetails
                               join ct in db.CodeTables on b.AmenitiesCodeId equals ct.KeyCode
                               where ct.Title.Equals("Amenities") && b.RouteDetailId.Equals(id)

                               select new RouteDetailDTO()
                               {
                                   RouteDetailId = b.RouteDetailId,
                                   RouteId = b.RouteId,
                                   AmenitiesCodeId = b.AmenitiesCodeId,
                                   Amenities = ct.Value

                               };

            RouteDetailDTO routeDetail = await routedetails.SingleOrDefaultAsync(r => r.RouteDetailId == id);
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