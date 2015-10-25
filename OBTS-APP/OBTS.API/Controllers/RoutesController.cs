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
    public class RoutesController : ApiController
    {
        private ApplicationDbContext  db = new ApplicationDbContext ();
        private string strBusType = OPTSEnum.ToString(OPTSEnum.Types.BusType);
        private string strBrand = OPTSEnum.ToString(OPTSEnum.Types.Brand);

        // GET: api/Routes
        public IQueryable<RouteDTO> GetRoutes()
        {

            var routes = from a in db.Routes
                    join ct in db.Buses on a.BusId equals  ct.BusId
                         join ct1 in db.Cities on a.Source_CityId equals ct1.CityId
                         join ct2 in db.Cities on a.Destination_CityId equals ct2.CityId
                    join ct3 in db.CodeTables on ct.BusType equals ct3.KeyCode
                         where ct3.Title.Equals(strBusType)
                    join ct4 in db.CodeTables on ct.Brand equals ct4.KeyCode
                         where ct4.Title.Equals(strBrand)

                         select new RouteDTO()
                                 {
                                    RouteId = a.RouteId,
                                    BusId=a.BusId,
                                    Company=ct.Company,
                                    Brand=ct.Brand,
                                    BrandDesc=ct4.Value,
                                    BusType=ct.BusType,
                                    BusTypeDesc=ct3.Value,
                                    RegistrationNo= ct.RegistrationNo,
                                    VechiclePhoneNo=ct.VechiclePhoneNo,
                                    Source_CityId = a.Source_CityId,
                                    SourceCity=ct1.CityDesc,
                                    Destination_CityId = a.Destination_CityId,
                                    DestinationCity=ct2.CityDesc,
                                    Recurrsive=a.Recurrsive,
                                    RouteDate=a.RouteDate,
                                    DepartureTime=a.DepartureTime,
                                    ArrivalTime=a.ArrivalTime,
                                    RouteFare=a.RouteFare
                                 };
            return routes;
        }

        // GET: api/bus/{Id}/routes
        [Route("api/bus/{Id}/routes", Name = "GetRoutesByBus")]
        public IQueryable<RouteDTO> GetRoutesByBus(Guid Id)
        {

            var routes = from a in db.Routes
                         join ct in db.Buses on a.BusId equals ct.BusId
                         where a.BusId.Equals(Id)
                         join ct1 in db.Cities on a.Source_CityId equals ct1.CityId
                         join ct2 in db.Cities on a.Destination_CityId equals ct2.CityId
                         join ct3 in db.CodeTables on ct.BusType equals ct3.KeyCode
                         where ct3.Title.Equals(strBusType)
                         join ct4 in db.CodeTables on ct.Brand equals ct4.KeyCode
                         where ct4.Title.Equals(strBrand)

                         select new RouteDTO()
                         {
                             RouteId = a.RouteId,
                             BusId = a.BusId,
                             Company = ct.Company,
                             Brand = ct.Brand,
                             BrandDesc = ct4.Value,
                             BusType = ct.BusType,
                             BusTypeDesc = ct3.Value,
                             VechiclePhoneNo = ct.VechiclePhoneNo,
                             RegistrationNo = ct.RegistrationNo,
                             Source_CityId = a.Source_CityId,
                             SourceCity = ct1.CityDesc,
                             Destination_CityId = a.Destination_CityId,
                             DestinationCity = ct2.CityDesc,
                             Recurrsive = a.Recurrsive,
                             RouteDate = a.RouteDate,
                             DepartureTime = a.DepartureTime,
                             ArrivalTime = a.ArrivalTime,
                             RouteFare = a.RouteFare
                         };
            return routes;
        }

        // GET: api/bus/{Id}/routes
        [Route("api/{_source}/{_destination}/routes", Name = "GetRoutesByCities")]
        public IQueryable<RouteDTO> GetRoutesByCities(string _source, string _destination)
        {

            var routes = from a in db.Routes
                         join ct in db.Buses on a.BusId equals ct.BusId
                         join ct1 in db.Cities on a.Source_CityId equals ct1.CityId
                         where ct1.CityDesc.Equals(_source)
                         join ct2 in db.Cities on a.Destination_CityId equals ct2.CityId
                         where ct2.CityDesc.Equals(_destination)
                         join ct3 in db.CodeTables on ct.BusType equals ct3.KeyCode
                         where ct3.Title.Equals(strBusType)
                         join ct4 in db.CodeTables on ct.Brand equals ct4.KeyCode
                         where ct4.Title.Equals(strBrand)

                         select new RouteDTO()
                         {
                             RouteId = a.RouteId,
                             BusId = a.BusId,
                             Company = ct.Company,
                             Brand = ct.Brand,
                             BrandDesc = ct4.Value,
                             BusType = ct.BusType,
                             BusTypeDesc = ct3.Value,
                             VechiclePhoneNo = ct.VechiclePhoneNo,
                             RegistrationNo = ct.RegistrationNo,
                             Source_CityId = a.Source_CityId,
                             SourceCity = ct1.CityDesc,
                             Destination_CityId = a.Destination_CityId,
                             DestinationCity = ct2.CityDesc,
                             Recurrsive = a.Recurrsive,
                             RouteDate = a.RouteDate,
                             DepartureTime = a.DepartureTime,
                             ArrivalTime = a.ArrivalTime,
                             RouteFare = a.RouteFare
                         };
            return routes;
        }

        // GET: api/Route/5
        [ResponseType(typeof(RouteDTO))]
        [Route("api/route/{Id}", Name = "GetRoute")]
        public async Task<IHttpActionResult> GetRoute(Guid id)
        {
            //Route route = await db.Routes.FindAsync(id);
            var routes = from a in db.Routes
                         join ct in db.Buses on a.BusId equals ct.BusId
                         where a.RouteId.Equals(id)
                         join ct1 in db.Cities on a.Source_CityId equals ct1.CityId
                         join ct2 in db.Cities on a.Destination_CityId equals ct2.CityId
                         join ct3 in db.CodeTables on ct.BusType equals ct3.KeyCode
                         where ct3.Title.Equals(strBusType)
                         join ct4 in db.CodeTables on ct.Brand equals ct4.KeyCode
                         where ct4.Title.Equals(strBrand)

                         select new RouteDTO()
                         {
                             RouteId = a.RouteId,
                             BusId = a.BusId,
                             Company = ct.Company,
                             Brand = ct.Brand,
                             BrandDesc = ct4.Value,
                             BusType = ct.BusType,
                             BusTypeDesc = ct3.Value,
                             VechiclePhoneNo = ct.VechiclePhoneNo,
                             RegistrationNo = ct.RegistrationNo,
                             Source_CityId = a.Source_CityId,
                             SourceCity = ct1.CityDesc,
                             Destination_CityId = a.Destination_CityId,
                             DestinationCity = ct2.CityDesc,
                             Recurrsive = a.Recurrsive,
                             RouteDate = a.RouteDate,
                             DepartureTime = a.DepartureTime,
                             ArrivalTime = a.ArrivalTime,
                             RouteFare = a.RouteFare
                         };

            RouteDTO route = await routes.SingleOrDefaultAsync(b => b.RouteId == id);
            if (route == null)
            {
                return NotFound();
            }

            return Ok(route);
        }

        // PUT: api/Routes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoute(Guid id, Route route)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != route.RouteId)
            {
                return BadRequest();
            }

            db.Entry(route).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteExists(id))
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

        // POST: api/Routes
        [ResponseType(typeof(Route))]
        public async Task<IHttpActionResult> PostRoute(Route route)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Routes.Add(route);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RouteExists(route.RouteId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = route.RouteId }, route);
        }

        // DELETE: api/Routes/5
        [ResponseType(typeof(Route))]
        public async Task<IHttpActionResult> DeleteRoute(Guid id)
        {
            Route route = await db.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            db.Routes.Remove(route);
            await db.SaveChangesAsync();

            return Ok(route);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RouteExists(Guid id)
        {
            return db.Routes.Count(e => e.RouteId == id) > 0;
        }
    }
}