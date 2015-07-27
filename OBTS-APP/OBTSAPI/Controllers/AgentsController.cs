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
using OBTSAPI.Models.DTO;

namespace OBTSAPI.Controllers
{
    public class AgentsController : ApiController
    {
        private OBTSDbContext db = new OBTSDbContext(ConfigurationManager.AppSettings["OBTSDb"].ToString());

        // GET: api/Agents
        public IQueryable<AgentDTO> GetAgents()
        {
            var agents = from b in db.Agents
                        join ct in db.Cities on b.City equals ct.CityId
                        join ctr in db.Countries on b.Country equals ctr.CountryId
                        join reg in db.Regions on b.StateRegion equals reg.RegionId

                        select new AgentDTO()
                        {
                            AgentId = b.AgentId,
                            Comapnay = b.Comapnay,
                            UserName = b.UserName,
                            AccountId = b.AccountId,
                            BalanceCredit = b.BalanceCredit,
                            Name = b.Name,
                            Email = b.Email,
                            PanNumber = b.PanNumber,
                            Address = b.Address,
                            City = b.City,
                            CityName = ct.CityDesc,
                            StateRegion=b.StateRegion,
                            StateRegionName=reg.RegionDesc,
                            PinCode = b.PinCode,
                            Country = b.Country,
                            CountryName= ctr.CountryDesc,
                            Mobile = b.Mobile,
                            OfficePhone=b.OfficePhone,
                            Fax = b.Fax,
                            UserName2 = b.UserName2,
                            Password = b.Password,
                            Logo =b.Logo,
                            Status=b.Status
                        };
            return agents;
            
        }

        // GET: api/Agents/5
        [ResponseType(typeof(Agent))]
        public async Task<IHttpActionResult> GetAgent(Guid id)
        {
            Agent agent = await db.Agents.FindAsync(id);
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