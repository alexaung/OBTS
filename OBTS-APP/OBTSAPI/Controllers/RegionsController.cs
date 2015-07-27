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
    public class RegionsController : ApiController
    {
        private OBTSDbContext db = new OBTSDbContext(ConfigurationManager.AppSettings["OBTSDb"].ToString());

        // Typed lambda expression for Select() method. 
        private static readonly Expression<Func<City, CitiesRegionDTO>> AsCitiesRegionDTO =
            x => new CitiesRegionDTO
            {
                CityId = x.CityId,
                CityDesc = x.CityDesc,
            };

        // GET: api/Regions
        public IQueryable<CountryRegionDTO> GetRegions()
        {
            var regions = from b in db.Regions
                          select new CountryRegionDTO()
                        {
                            RegionId = b.RegionId,
                            RegionDesc = b.RegionDesc,
                            CountryId= b.CountryId,
                            CountryDesc = b.Country.CountryDesc
                        };
            return regions;
        }

        // GET: api/regions/1/cities
        [ActionName("cities")]
        public IQueryable<CitiesRegionDTO> GetCitiesByRegion(Guid Id)
        {
            // City city = await db.Cities.FindAsync(id);
            return db.Cities.Include(b => b.CountryRegion)
            .Where(b => b.CountryRegion.RegionId == Id)
            .Select(AsCitiesRegionDTO);
        }

        // GET: api/regions/region/1
        [ResponseType(typeof(CountryRegionDTO))]
        //[ActionName("region")]
        [Route("api/regions/{Id}", Name = "GetCountryRegion")]
        public async Task<IHttpActionResult> GetCountryRegion(Guid id)
        {
            var countryRegion = await db.Regions.Include(b => b.Country).Select(b =>
               new CountryRegionDTO()
               {
                   RegionId = b.RegionId,
                   RegionDesc = b.RegionDesc,
                   CountryId = b.CountryId,
                   CountryDesc = b.Country.CountryDesc
               }).SingleOrDefaultAsync(b => b.RegionId == id);
            if (countryRegion == null)
            {
                return NotFound();
            }

            return Ok(countryRegion);
        }

        // PUT: api/CountryRegions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCountryRegion(Guid id, Region countryRegion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != countryRegion.RegionId)
            {
                return BadRequest();
            }

            db.Entry(countryRegion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryRegionExists(id))
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

        // POST: api/CountryRegions
        [ResponseType(typeof(Region))]
        public async Task<IHttpActionResult> PostCountryRegion(Region countryRegion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Regions.Add(countryRegion);
            await db.SaveChangesAsync();

            // Load country 
            db.Entry(countryRegion).Reference(x => x.Country).Load();

            var dto = new CountryRegionDTO()
            {
                RegionId = countryRegion.RegionId,
                RegionDesc = countryRegion.RegionDesc,
                CountryId = countryRegion.CountryId,
                CountryDesc = countryRegion.Country.CountryDesc
            };
            return CreatedAtRoute("DefaultApi", new { id = countryRegion.RegionId }, countryRegion);
        }

        // DELETE: api/CountryRegions/5
        [ResponseType(typeof(Region))]
        public async Task<IHttpActionResult> DeleteCountryRegion(Guid id)
        {
            Region countryRegion = await db.Regions.FindAsync(id);
            if (countryRegion == null)
            {
                return NotFound();
            }

            db.Regions.Remove(countryRegion);
            await db.SaveChangesAsync();

            return Ok(countryRegion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryRegionExists(Guid id)
        {
            return db.Regions.Count(e => e.RegionId == id) > 0;
        }
    }
}