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
    public class OperatorAgentsController : ApiController
    {
        private OBTSDbContext db = new OBTSDbContext(ConfigurationManager.AppSettings["OBTSDb"].ToString());

        // GET: api/OperatorAgents
        public IQueryable<OperatorAgentDTO> GetOperatorAgents()
        {

            var agents = from b in db.OperatorAgents
                        join a in db.Agents on b.AgentId equals a.AgentId
                        join o in db.Operators on b.OperatorId equals o.OperatorId
                        
                        select new OperatorAgentDTO()
                        {
                            OperatorAgentId = b.OperatorAgentId,
                            OperatorId = b.OperatorId,
                            OperatorName = o.Company,
                            AgentId = b.AgentId,
                            AgentName = a.Comapnay,
                            DepositAmt = b.DepositAmt,
                            JointDate = b.JointDate,
                            CreatedDate = b.CreatedDate


                        };
            return agents;
        }

        // GET: api/Buses
        [Route("api/operator/{Id}/agents", Name = "GetAgentsByOperator")]
        public IQueryable<AgentDTO> GetAgentsByOperator(Guid Id)
        {

            var agents = from b in db.OperatorAgents
                         join a in db.Agents on b.AgentId equals a.AgentId
                         join o in db.Operators on b.OperatorId equals o.OperatorId
                         join ct in db.Cities on a.City equals ct.CityId
                         join ctr in db.Countries on a.Country equals ctr.CountryId
                         join reg in db.Regions on a.StateRegion equals reg.RegionId
                         where b.OperatorId.Equals(Id)

                         select new AgentDTO()
                         {
                             AgentId = b.AgentId,
                             Comapnay = a.Comapnay,
                             UserName = a.UserName,
                             AccountId = a.AccountId,
                             BalanceCredit = a.BalanceCredit,
                             Name = a.Name,
                             Email = a.Email,
                             PanNumber = a.PanNumber,
                             Address = a.Address,
                             City = a.City,
                             CityName = ct.CityDesc,
                             StateRegion = a.StateRegion,
                             StateRegionName = reg.RegionDesc,
                             PinCode = a.PinCode,
                             Country = a.Country,
                             CountryName = ctr.CountryDesc,
                             Mobile = a.Mobile,
                             OfficePhone = a.OfficePhone,
                             Fax = a.Fax,
                             UserName2 = a.UserName2,
                             Password = a.Password,
                             Logo = a.Logo,
                             Status = a.Status
                         };
            return agents;
        }

        // GET: api/Buses
        [Route("api/agent/{Id}/operators", Name = "GetOperatorsByAgent")]
        public IQueryable<OperatorAgentDTO> GetOperatorsByAgent(Guid Id)
        {

            var operators = from b in db.OperatorAgents
                         join a in db.Agents on b.AgentId equals a.AgentId
                         join o in db.Operators on b.OperatorId equals o.OperatorId
                         where b.AgentId.Equals(Id)

                         select new OperatorAgentDTO()
                         {
                             OperatorAgentId = b.OperatorAgentId,
                             OperatorId = b.OperatorId,
                             OperatorName = o.Company,
                             AgentId = b.AgentId,
                             AgentName = a.Comapnay,
                             DepositAmt = b.DepositAmt,
                             JointDate = b.JointDate,
                             CreatedDate = b.CreatedDate


                         };
            return operators;
        }


        // GET: api/OperatorAgents/5
        [ResponseType(typeof(OperatorAgent))]
        public async Task<IHttpActionResult> GetOperatorAgent(Guid id)
        {
            OperatorAgent operatorAgent = await db.OperatorAgents.FindAsync(id);
            if (operatorAgent == null)
            {
                return NotFound();
            }

            return Ok(operatorAgent);
        }

        // PUT: api/OperatorAgents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOperatorAgent(Guid id, OperatorAgent operatorAgent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != operatorAgent.OperatorAgentId)
            {
                return BadRequest();
            }

            db.Entry(operatorAgent).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperatorAgentExists(id))
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

        // POST: api/OperatorAgents
        [ResponseType(typeof(OperatorAgent))]
        public async Task<IHttpActionResult> PostOperatorAgent(OperatorAgent operatorAgent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OperatorAgents.Add(operatorAgent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OperatorAgentExists(operatorAgent.OperatorAgentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = operatorAgent.OperatorAgentId }, operatorAgent);
        }

        // DELETE: api/OperatorAgents/5
        [ResponseType(typeof(OperatorAgent))]
        public async Task<IHttpActionResult> DeleteOperatorAgent(Guid id)
        {
            OperatorAgent operatorAgent = await db.OperatorAgents.FindAsync(id);
            if (operatorAgent == null)
            {
                return NotFound();
            }

            db.OperatorAgents.Remove(operatorAgent);
            await db.SaveChangesAsync();

            return Ok(operatorAgent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OperatorAgentExists(Guid id)
        {
            return db.OperatorAgents.Count(e => e.OperatorAgentId == id) > 0;
        }
    }
}