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

namespace OBTS.API.Controllers
{
    public class DummiesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Dummies
        public IQueryable<Dummy> GetDummies()
        {
            return db.Dummies;
        }

        // GET: api/Dummies/5
        [ResponseType(typeof(Dummy))]
        public async Task<IHttpActionResult> GetDummy(int id)
        {
            Dummy dummy = await db.Dummies.FindAsync(id);
            if (dummy == null)
            {
                return NotFound();
            }

            return Ok(dummy);
        }

        // PUT: api/Dummies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDummy(int id, Dummy dummy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dummy.DummyId)
            {
                return BadRequest();
            }

            db.Entry(dummy).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DummyExists(id))
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

        // POST: api/Dummies
        [ResponseType(typeof(Dummy))]
        public async Task<IHttpActionResult> PostDummy(Dummy dummy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dummies.Add(dummy);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dummy.DummyId }, dummy);
        }

        // DELETE: api/Dummies/5
        [ResponseType(typeof(Dummy))]
        public async Task<IHttpActionResult> DeleteDummy(int id)
        {
            Dummy dummy = await db.Dummies.FindAsync(id);
            if (dummy == null)
            {
                return NotFound();
            }

            db.Dummies.Remove(dummy);
            await db.SaveChangesAsync();

            return Ok(dummy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DummyExists(int id)
        {
            return db.Dummies.Count(e => e.DummyId == id) > 0;
        }
    }
}