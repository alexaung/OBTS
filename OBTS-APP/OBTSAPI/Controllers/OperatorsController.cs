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

namespace OBTSAPI.Controllers
{
    public class OperatorsController : ApiController
    {
        private OBTSDbContext db = new OBTSDbContext(ConfigurationManager.AppSettings["OBTSDb"].ToString());

        // GET: api/Operators
        public IQueryable<Operator> GetOperators()
        {
            return db.Operators;
        }

        // GET: api/Operators/5
        [ResponseType(typeof(Operator))]
        public async Task<IHttpActionResult> GetOperator(Guid id)
        {
            Operator @operator = await db.Operators.FindAsync(id);
            if (@operator == null)
            {
                return NotFound();
            }

            return Ok(@operator);
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