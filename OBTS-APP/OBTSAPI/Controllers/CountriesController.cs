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
using OBTSAPI.Models;
using OBTSAPI.DbContexts;
using System.Configuration;
using System.Linq.Expressions;
using OBTSAPI.Models.DTO;

namespace OBTSAPI.Controllers
{
    public class CountriesController : ApiController
    {
        private OBTSDbContext db = new OBTSDbContext(ConfigurationManager.AppSettings["OBTSDb"].ToString());

        // Typed lambda expression for Select() method. 
        private static readonly Expression<Func<Region, RegionsCountryDTO>> AsRegionsCountryDTO =
            a => new RegionsCountryDTO
            {
                RegionId = a.RegionId,
                RegionDesc = a.RegionDesc,
            };

        // Typed lambda expression for Select() method. 
        private static readonly Expression<Func<City, CitiesCountryDTO>> AsCitiesCountryDTO =
            x => new CitiesCountryDTO
            {
                CityId = x.CityId,
                CityDesc = x.CityDesc,
                RegionId = x.RegionId,
                RegionDesc = x.CountryRegion.RegionDesc,
            };

        // GET: api/Countries
        public IQueryable<Country> GetCountries()
        {
            return db.Countries;
        }

        // GET: api/country/1/cities
        [ActionName("cities")]
        public IQueryable<CitiesCountryDTO> GetCitiesByCountry(Guid Id)
        {
            // City city = await db.Cities.FindAsync(id);
            return db.Cities.Include(b => b.CountryRegion)
            .Where(b => b.CountryRegion.CountryId == Id)
            .Select(AsCitiesCountryDTO);
        }

        // GET: api/country/1/regions
        [Route("api/countries/{Id}", Name = "GetRegionsByCountry")]
        public IQueryable<RegionsCountryDTO> GetRegionsByCountry(Guid Id)
        {
            // City city = await db.Cities.FindAsync(id);
            return db.Regions
            .Where(b => b.CountryId == Id)
            .Select(AsRegionsCountryDTO);
        }

        // GET: api/countries/country/1
        [ResponseType(typeof(Country))]
        [ActionName("country")]
        public async Task<IHttpActionResult> GetCountry(Guid id)
        {
            Country country = await db.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // PUT: api/Countries/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCountry(Guid id, Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != country.CountryId)
            {
                return BadRequest();
            }

            db.Entry(country).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // POST: api/Countries
        [ResponseType(typeof(Country))]
        public async Task<IHttpActionResult> PostCountry(Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Countries.Add(country);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = country.CountryId }, country);
        }

        // DELETE: api/Countries/5
        [ResponseType(typeof(Country))]
        public async Task<IHttpActionResult> DeleteCountry(Guid id)
        {
            Country country = await db.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            db.Countries.Remove(country);
            await db.SaveChangesAsync();

            return Ok(country);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryExists(Guid id)
        {
            return db.Countries.Count(e => e.CountryId == id) > 0;
        }
    }
}