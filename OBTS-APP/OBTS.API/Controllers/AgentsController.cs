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
//using OBTS.API.DbContexts;
using OBTS.API.Models;
using System.Configuration;
using OBTS.API.Models.DTO;

namespace OBTS.API.Controllers
{
    public class AgentsController : ApiController
    {
        private ApplicationDbContext  db = new ApplicationDbContext ();

        // GET: api/Agents
        [ResponseType(typeof(AgentDTO))]
        public async Task<IHttpActionResult> GetAgents()
        {

            var agents = await db.Agents
                .Include(a => a._city)
                .Include(a => a._city.CountryRegion)
                .Include(a => a._city.CountryRegion.Country)
                .Select(a =>
                        new AgentDTO()
                        {
                            AgentId = a.AgentId,
                            Comapany = a.Comapany,
                            UserName = a.UserName,
                            AccountId = a.AccountId,
                            BalanceCredit = a.BalanceCredit,
                            Name = a.Name,
                            Email = a.Email,
                            PanNumber = a.PanNumber,
                            Address = a.Address,
                            CityId = a.CityId,
                            CityName = a._city.CityDesc,
                            RegionId = a._city.CountryRegion.RegionId,
                            RegionName = a._city.CountryRegion.RegionDesc,
                            PinCode = a.PinCode,
                            CountryId = a._city.CountryRegion.Country.CountryId,
                            CountryName = a._city.CountryRegion.Country.CountryDesc,
                            Mobile = a.Mobile,
                            OfficePhone = a.OfficePhone,
                            Fax = a.Fax,
                            UserName2 = a.UserName2,
                            Password = a.Password,
                            Logo = a.Logo,
                            Status = a.Status
                        }).ToListAsync();
            return Ok(agents.AsQueryable());
            
        }

        /*
        // GET: api/agent/{Id}/operators
        [Route("api/agent/{Id}/operators", Name = "GetOperatorsByAgent")]
        public IQueryable<OperatorDTO> GetOperatorsByAgent(Guid Id)
        {
            var _operators = from o in db.Operators
                .Include(o => o._city)
                .Include(o => o._region)
                .Include(o => o._country)
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
        }*/

        // GET: api/Agents/5
        [ResponseType(typeof(Agent))]
        [Route("api/agent/{Id}", Name = "GetAgent")]
        public async Task<IHttpActionResult> GetAgent(Guid id)
        {
            var agent = await db.Agents
                .Include(a => a._city)
                .Include(a => a._city.CountryRegion)
                .Include(a => a._city.CountryRegion.Country)
                .Select(a=>
                
                    new AgentDTO()
                        {
                            AgentId = a.AgentId,
                            Comapany = a.Comapany,
                            UserName = a.UserName,
                            AccountId = a.AccountId,
                            BalanceCredit = a.BalanceCredit,
                            Name = a.Name,
                            Email = a.Email,
                            PanNumber = a.PanNumber,
                            Address = a.Address,
                            CityId = a.CityId,
                            CityName = a._city.CityDesc,
                            RegionId=a._city.CountryRegion.RegionId,
                            RegionName=a._city.CountryRegion.RegionDesc,
                            PinCode = a.PinCode,
                            CountryId = a._city.CountryRegion.Country.CountryId,
                            CountryName= a._city.CountryRegion.Country.CountryDesc,
                            Mobile = a.Mobile,
                            OfficePhone=a.OfficePhone,
                            Fax = a.Fax,
                            UserName2 = a.UserName2,
                            Password = a.Password,
                            Logo =a.Logo,
                            Status=a.Status
                        }).SingleOrDefaultAsync(a => a.AgentId == id);

            if (agent == null)
            {
                return NotFound();
            }

            return Ok(agent);
        }

        // PUT: api/Agents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAgent(Guid id, Agent agent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agent.AgentId)
            {
                return BadRequest();
            }

            db.Entry(agent).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentExists(id))
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

        // POST: api/Agents
        [ResponseType(typeof(Agent))]
        public async Task<IHttpActionResult> PostAgent(Agent agent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            agent.AgentId = Guid.NewGuid();
            db.Agents.Add(agent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AgentExists(agent.AgentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = agent.AgentId }, agent);
        }

        // DELETE: api/Agents/5
        [ResponseType(typeof(Agent))]
        public async Task<IHttpActionResult> DeleteAgent(Guid id)
        {
            Agent agent = await db.Agents.FindAsync(id);
            if (agent == null)
            {
                return NotFound();
            }

            db.Agents.Remove(agent);
            await db.SaveChangesAsync();

            return Ok(agent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AgentExists(Guid id)
        {
            return db.Agents.Count(e => e.AgentId == id) > 0;
        }
    }
}