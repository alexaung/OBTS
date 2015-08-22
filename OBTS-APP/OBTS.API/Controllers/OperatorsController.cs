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
using OBTS.API;
using OBTS.API.Models;
using System.Configuration;
using OBTS.API.Models.DTO;

namespace OBTS.API.Controllers
{
    public class OperatorsController : ApiController
    {
        private ApplicationDbContext  db = new ApplicationDbContext ();


        // GET: api/Operators
        public IQueryable<OperatorDTO> GetOperators()
        {
            var _operators = from o in db.Operators
                .Include(o => o._city)
               // .Include(o=> o._city.CountryRegion)
               // .Include(o=> o._city.CountryRegion.Country)
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
                    CountryId = o._city.CountryRegion.Country.CountryId,
                    CountryName = o._city.CountryRegion.Country.CountryDesc,
                    RegionId = o._city.CountryRegion.RegionId,
                    RegionName = o._city.CountryRegion.RegionDesc,
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

        // GET: api/Operator/5
        [ResponseType(typeof(OperatorDTO))]
        [Route("api/operator/{Id}", Name = "GetOperator")]
        public async Task<IHttpActionResult> GetOperator(Guid id)
        {
            var _operator = await db.Operators
                .Include(o => o._city)
                //.Include(o=> o._city.CountryRegion)
                //.Include(o=> o._city.CountryRegion.Country)
                .Select(o =>

            new OperatorDTO()
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
                                CountryId = o._city.CountryRegion.Country.CountryId,
                                CountryName = o._city.CountryRegion.Country.CountryDesc,
                                RegionId = o._city.CountryRegion.RegionId,
                                RegionName = o._city.CountryRegion.RegionDesc,
                                CityId = o.CityId,
                                CityName = o._city.CityDesc,
                                NumberOfBuses = o.NumberOfBuses,
                                NumberOfRoutes = o.NumberOfRoutes,
                                Status = o.Status,
                                UserName = o.UserName,
                                Password = o.Password
                            }).SingleOrDefaultAsync(o => o.OperatorId == id);

            if (_operator == null)
            {
                return NotFound();
            }

            return Ok(@_operator);
        }

        // PUT: api/Operators/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOperator(Guid id, Operator @operator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @operator.OperatorId)
            {
                return BadRequest();
            }

            db.Entry(@operator).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperatorExists(id))
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

        // POST: api/Operators
        [ResponseType(typeof(Operator))]
        public async Task<IHttpActionResult> PostOperator(Operator @operator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Operators.Add(@operator);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OperatorExists(@operator.OperatorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = @operator.OperatorId }, @operator);
        }

        // DELETE: api/Operators/5
        [ResponseType(typeof(Operator))]
        public async Task<IHttpActionResult> DeleteOperator(Guid id)
        {
            Operator @operator = await db.Operators.FindAsync(id);
            if (@operator == null)
            {
                return NotFound();
            }

            db.Operators.Remove(@operator);
            await db.SaveChangesAsync();

            return Ok(@operator);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OperatorExists(Guid id)
        {
            return db.Operators.Count(e => e.OperatorId == id) > 0;
        }
    }
}