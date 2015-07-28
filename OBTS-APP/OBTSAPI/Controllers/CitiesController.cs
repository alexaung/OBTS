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
using OBTSAPI.DbContexts;
using OBTSAPI.Models;
using System.Configuration;
using System.Linq.Expressions;
using OBTSAPI.Models.DTO;

namespace OBTSAPI.Controllers
{
   
    public class CitiesController : ApiController
    {
        private OBTSDbContext db = new OBTSDbContext(ConfigurationManager.AppSettings["OBTSDb"].ToString());

        // GET: api/Cities
        
        public IQueryable<CityDTO> GetCities()
        {
            var cities = from b in db.Cities
                         select new CityDTO()
                            {
                                CityId = b.CityId,
                                CityDesc = b.CityDesc,
                                RegionId = b.RegionId,
                                RegionDesc = b.CountryRegion.RegionDesc,
                                CountryId= b.CountryRegion.Country.CountryId,
                                CountryDesc=b.CountryRegion.Country.CountryDesc
                            };
            return cities;
        }

        // GET: api/city/{Id}/operators
        [Route("api/city/{Id}/operators", Name = "GetOperatorsByCity")]
        public IQueryable<OperatorDTO> GetOperatorsByCity(Guid Id)
        {
            var _operators = from o in db.Operators
                .Include(o => o._city)
                .Include(o => o._region)
                .Include(o => o._country)
                .Where(o => o.CityId == Id)
                             select new OperatorDTO()
                             {
                                 OperatorId = o.OperatorId,
                                 FirstName = o.FirstName,
                                 LastName = o.LastName,
                                 Mobile = o.Mobile,
                                 EmailAddress = o.EmailAddress,
                                 PhoneNumber = o.PhoneNumber,
                                 Company = o.Company,
                                 CompanyPhone = o.CompanyPhone,
                                 Address = o.Address,
                                 CountryId = o.CountryId,
                                 CountryName = o._country.CountryDesc,
                                 RegionId = o.RegionId,
                                 RegionName = o._region.RegionDesc,
                                 CityId = o.CityId,
                                 CityName = o._city.CityDesc,
                                 NumberOfBuses = o.NumberOfBuses,
                                 NumberOfRoutes = o.NumberOfRoutes,
                                 Status = o.Status,
                                 UserName = o.UserName,
                                 Password = o.Password
                             };


            return _operators;
        }

        // GET: api/Cities/5
        [ResponseType(typeof(CityDTO))]
        [Route("api/city/{Id}", Name = "GetCity")]
        public async Task<IHttpActionResult> GetCity(Guid id)
        {
           // City city = await db.Cities.FindAsync(id);

            var city = await db.Cities.Include(b => b.CountryRegion).Select(b =>
               new CityDTO()
               {
                   CityId = b.CityId,
                   CityDesc = b.CityDesc,
                   RegionId = b.RegionId,
                   RegionDesc = b.CountryRegion.RegionDesc,
                   CountryId = b.CountryRegion.Country.CountryId,
                   CountryDesc = b.CountryRegion.Country.CountryDesc
               }).SingleOrDefaultAsync(b => b.CityId == id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // PUT: api/Cities/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCity(Guid id, City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != city.CityId)
            {
                return BadRequest();
            }

            db.Entry(city).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/Cities
        [ResponseType(typeof(City))]
        public async Task<IHttpActionResult> PostCity(City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cities.Add(city);
            await db.SaveChangesAsync();

            // Load country region
            db.Entry(city).Reference(x => x.CountryRegion).Load();

            var dto = new CityDTO()
            {
                   CityId = city.CityId,
                   CityDesc = city.CityDesc,
                   RegionId = city.RegionId,
                   RegionDesc = city.CountryRegion.RegionDesc,
                   CountryId = city.CountryRegion.Country.CountryId,
                   CountryDesc = city.CountryRegion.Country.CountryDesc
            };

            return CreatedAtRoute("DefaultApi", new { id = city.CityId }, city);
        }

        // DELETE: api/Cities/5
        [ResponseType(typeof(City))]
        public async Task<IHttpActionResult> DeleteCity(Guid id)
        {
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            db.Cities.Remove(city);
            await db.SaveChangesAsync();

            return Ok(city);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CityExists(Guid id)
        {
            return db.Cities.Count(e => e.CityId == id) > 0;
        }
    }
}