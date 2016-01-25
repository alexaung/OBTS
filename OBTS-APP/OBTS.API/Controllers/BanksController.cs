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
using OBTS.API.Models;
using OBTS.API.Models.DTO;

namespace OBTS.API.Controllers
{
    public class BanksController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string strUserType = OBTSEnum.ToString(OBTSEnum.Types.UserType);
        private string strOperator = OBTSEnum.ToString(OBTSEnum.Types.Operator);

        // GET: api/Banks
        [ResponseType(typeof(BankDTO))]
        public async Task<IHttpActionResult> GetBanks()
        {

            var banks = await (from b in db.Banks
                               join ct in db.CodeTables on b.UserTypeCode equals ct.KeyCode
                               from o in db.Operators
                               .Where(w => w.OperatorId == b.UserId).DefaultIfEmpty()
                               from a in db.Agents
                               .Where(x => x.AgentId == b.UserId).DefaultIfEmpty()
                               where ct.Title.Equals(strUserType)

                               select new BankDTO()
                               {
                                   BankId = b.BankId,
                                   UserId = b.UserId,
                                   UserName = (ct.Value.Equals(strOperator) ? o.Company : a.Comapany),
                                   UserTypeDesc = ct.Value,
                                   BankName = b.BankName,
                                   Branch = b.Branch,
                                   AccountNumber = b.AccountNumber,
                                   LFSCCode = b.LFSCCode,
                                   Logo = b.Logo
                               }
                        ).ToListAsync();


            return Ok(banks.AsQueryable());
        }

        // GET: api/Banks/5
        [ResponseType(typeof(Bank))]
        public async Task<IHttpActionResult> GetBank(Guid id)
        {
            Bank bank = await db.Banks.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }

            return Ok(bank);
        }

        // PUT: api/Banks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBank(Guid id, Bank bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bank.BankId)
            {
                return BadRequest();
            }

            db.Entry(bank).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankExists(id))
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

        // POST: api/Banks
        [ResponseType(typeof(Bank))]
        public async Task<IHttpActionResult> PostBank(Bank bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bank.BankId = Guid.NewGuid();
            db.Banks.Add(bank);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BankExists(bank.BankId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bank.BankId }, bank);
        }

        // DELETE: api/Banks/5
        [ResponseType(typeof(Bank))]
        public async Task<IHttpActionResult> DeleteBank(Guid id)
        {
            Bank bank = await db.Banks.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }

            db.Banks.Remove(bank);
            await db.SaveChangesAsync();

            return Ok(bank);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BankExists(Guid id)
        {
            return db.Banks.Count(e => e.BankId == id) > 0;
        }
    }
}