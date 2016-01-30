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
    public class ContactDetailsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ContactDetails
        [ResponseType(typeof(ContactDetail))]
        public async Task<IHttpActionResult> GetContactDetails()
        {
            return Ok((await (db.ContactDetails).ToListAsync()).AsQueryable());
        }

        // GET: api/route/{RouteId}/contactdetails
        [Route("api/route/{RouteId}/contactdetails", Name = "GetContactDetailsByRouteId")]
        [ResponseType(typeof(ContactDetailDTO))]
        public async Task<IHttpActionResult> GetContactDetailsByRouteId(Guid RouteId)
        {
            ContactDetail contactDetail= await db.ContactDetails.Where(u => u.RouteId == RouteId).FirstOrDefaultAsync();
            //await contactDetail.FirstOrDefaultAsync();
            if (contactDetail == null)
            {
                return NotFound();
            }

            List<Passenger> passengers = await db.Passengers.Where(u => u.ContactDetailId == contactDetail.ContactDetailId).ToListAsync();

            if (passengers == null)
            {
                return NotFound();
            }

            ContactDetailDTO dto = new ContactDetailDTO();
            dto.ContactDetailId = contactDetail.ContactDetailId;
            dto.Email = contactDetail.Email;
            dto.Mobile = contactDetail.Mobile;
            dto.RouteId = contactDetail.RouteId;
            dto.Passengers = passengers;

            return Ok(dto);
        }

        // GET: api/ContactDetails/5
        [ResponseType(typeof(ContactDetail))]
        public async Task<IHttpActionResult> GetContactDetail(Guid id)
        {
            ContactDetail contactDetail = await db.ContactDetails.FindAsync(id);
            if (contactDetail == null)
            {
                return NotFound();
            }

            return Ok(contactDetail);
        }

        // PUT: api/ContactDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContactDetail(Guid id, ContactDetail contactDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactDetail.ContactDetailId)
            {
                return BadRequest();
            }

            db.Entry(contactDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactDetailExists(id))
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

        // POST: api/ContactDetails
        [ResponseType(typeof(ContactDetail))]
        public async Task<IHttpActionResult> PostContactDetail(ContactDetail contactDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            contactDetail.ContactDetailId = Guid.NewGuid();	
            db.ContactDetails.Add(contactDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactDetailExists(contactDetail.ContactDetailId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = contactDetail.ContactDetailId }, contactDetail);
        }

        // DELETE: api/ContactDetails/5
        [ResponseType(typeof(ContactDetail))]
        public async Task<IHttpActionResult> DeleteContactDetail(Guid id)
        {
            ContactDetail contactDetail = await db.ContactDetails.FindAsync(id);
            if (contactDetail == null)
            {
                return NotFound();
            }

            db.ContactDetails.Remove(contactDetail);
            await db.SaveChangesAsync();

            return Ok(contactDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactDetailExists(Guid id)
        {
            return db.ContactDetails.Count(e => e.ContactDetailId == id) > 0;
        }
    }
}