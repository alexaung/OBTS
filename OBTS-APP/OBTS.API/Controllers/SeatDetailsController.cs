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
    public class SeatDetailsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SeatDetails
        public IQueryable<SeatDetail> GetSeatDetails()
        {
            return db.SeatDetails;
        }

        // GET: api/SeatDetails/5
        [ResponseType(typeof(SeatDetail))]
        public async Task<IHttpActionResult> GetSeatDetail(Guid id)
        {
            SeatDetail seatDetail = await db.SeatDetails.FindAsync(id);
            if (seatDetail == null)
            {
                return NotFound();
            }

            return Ok(seatDetail);
        }

        

        // PUT: api/SeatDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSeatDetail(Guid id, SeatDetail seatDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != seatDetail.SeatDetailId)
            {
                return BadRequest();
            }

            db.Entry(seatDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatDetailExists(id))
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

        // POST: api/SeatDetails
        [ResponseType(typeof(SeatDetail))]
        public async Task<IHttpActionResult> PostSeatDetail(SeatDetail seatDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SeatDetails.Add(seatDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SeatDetailExists(seatDetail.SeatDetailId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = seatDetail.SeatDetailId }, seatDetail);
        }

        // DELETE: api/SeatDetails/5
        [ResponseType(typeof(SeatDetail))]
        public async Task<IHttpActionResult> DeleteSeatDetail(Guid id)
        {
            SeatDetail seatDetail = await db.SeatDetails.FindAsync(id);
            if (seatDetail == null)
            {
                return NotFound();
            }

            db.SeatDetails.Remove(seatDetail);
            await db.SaveChangesAsync();

            return Ok(seatDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SeatDetailExists(Guid id)
        {
            return db.SeatDetails.Count(e => e.SeatDetailId == id) > 0;
        }
    }
}