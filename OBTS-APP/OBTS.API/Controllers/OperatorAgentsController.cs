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
//using OBTS.API.DbContexts;
using OBTS.API.Models;
using System.Configuration;
using OBTS.API.Models.DTO;

namespace OBTS.API.Controllers
{
    public class OperatorAgentsController : ApiController
    {
        private OBTSAPIContext db = new OBTSAPIContext();

        // GET: api/OperatorAgents
        public IQueryable<OperatorAgentDTO> GetOperatorAgents()
        {
            var agents = from o in db.OperatorAgents
                .Include(o => o._agent)
                .Include(o=> o._operator)
                             
                        select new OperatorAgentDTO()
                        {
                            OperatorAgentId = o.OperatorAgentId,
                            OperatorId = o.OperatorId,
                            OperatorName = o._operator.Company,
                            AgentId = o.AgentId,
                            AgentName = o._agent.Comapany,
                            DepositAmt = o.DepositAmt,
                            JointDate = o.JointDate,
                            CreatedDate = o.CreatedDate
                        };
            return agents;
        }

        // GET: api/operator/{Id}/agents
        [Route("api/operator/{Id}/agents", Name = "GetAgentsByOperator")]
        public IQueryable<AgentsOperatorDTO> GetAgentsByOperator(Guid Id)
        {
            var agents = from o in db.OperatorAgents
                .Include(o => o._agent)
                .Include(o=> o._operator)
                .Where (o=>o.OperatorId.Equals(Id))

                         select new AgentsOperatorDTO()
                         {
                             AgentId = o.AgentId,
                             Comapany = o._agent.Comapany,
                             UserName = o._agent.UserName,
                             AccountId = o._agent.AccountId,
                             BalanceCredit = o._agent.BalanceCredit,
                             Name = o._agent.Name,
                             Email = o._agent.Email,
                             PanNumber = o._agent.PanNumber,
                             Address = o._agent.Address,
                             CityId = o._agent.CityId,
                             CityName = o._agent._city.CityDesc,
                             RegionId = o._agent._city.CountryRegion.RegionId,
                             RegionName = o._agent._city.CountryRegion.RegionDesc,
                             PinCode = o._agent.PinCode,
                             CountryId = o._agent._city.CountryRegion.Country.CountryId,
                             CountryName = o._agent._city.CountryRegion.Country.CountryDesc,
                             Mobile = o._agent.Mobile,
                             OfficePhone = o._agent.OfficePhone,
                             Fax = o._agent.Fax,
                             UserName2 = o._agent.UserName2,
                             Password = o._agent.Password,
                             Logo = o._agent.Logo,
                             Status = o._agent.Status,
                             OperatorAgentId=o.OperatorAgentId,
                             OperatorId = o.OperatorId,
                             OperatorName= o._operator.Company,
                             DepositAmt = o.DepositAmt,
                             JointDate = o.JointDate,
                             CreatedDate = o.CreatedDate
                         };
            return agents;
        }

        // GET: api/agent/{Id}/operators
        [Route("api/agent/{Id}/operators", Name = "GetOperatorsByAgent")]
        public IQueryable<OperatorsAgentDTO> GetOperatorsByAgent(Guid Id)
        {
            var operators = from o in db.OperatorAgents
                .Include(o => o._agent)
                .Include(o => o._operator)
                .Where(o => o.AgentId.Equals(Id))

                            select new OperatorsAgentDTO()
                         {
                             OperatorId = o.OperatorId,
                             FirstName = o._operator.FirstName,
                             LastName = o._operator.LastName,
                             Mobile = o._operator.Mobile,
                             EmailAddress = o._operator.EmailAddress,
                             PhoneNumber = o._operator.PhoneNumber,
                             Company = o._operator.Company,
                             CompanyPhone = o._operator.CompanyPhone,
                             Address = o._operator.Address,
                             CountryId = o._operator._city.CountryRegion.Country.CountryId,
                             CountryName = o._operator._city.CountryRegion.Country.CountryDesc,
                             RegionId = o._operator._city.CountryRegion.RegionId,
                             RegionName = o._operator._city.CountryRegion.RegionDesc,
                             CityId = o._operator.CityId,
                             CityName = o._operator._city.CityDesc,
                             NumberOfBuses = o._operator.NumberOfBuses,
                             NumberOfRoutes = o._operator.NumberOfRoutes,
                             Status = o._operator.Status,
                             UserName = o._operator.UserName,
                             Password = o._operator.Password,
                             OperatorAgentId = o.OperatorAgentId,
                             AgentId = o.AgentId,
                             AgentName = o._agent.Comapany,
                             DepositAmt = o.DepositAmt,
                             JointDate = o.JointDate,
                             CreatedDate = o.CreatedDate
                         };
            return operators;
        }

        /*
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
        }*/


        // GET: api/OperatorAgents/5
        [ResponseType(typeof(OperatorAgent))]
        [Route("api/operatoragent/{Id}", Name = "GetOperatorAgent")]
        public async Task<IHttpActionResult> GetOperatorAgent(Guid id)
        {
           
            var agent = await db.OperatorAgents
                .Include(o => o._agent)
                .Include(o => o._operator)
                .Select(o=>
                         new OperatorAgentDTO()
                         {
                             OperatorAgentId = o.OperatorAgentId,
                             OperatorId = o.OperatorId,
                             OperatorName = o._operator.Company,
                             AgentId = o.AgentId,
                             AgentName = o._agent.Comapany,
                             DepositAmt = o.DepositAmt,
                             JointDate = o.JointDate,
                             CreatedDate = o.CreatedDate
                         }).SingleOrDefaultAsync(o => o.OperatorAgentId == id);


            if (agent == null)
            {
                return NotFound();
            }

            return Ok(agent);
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