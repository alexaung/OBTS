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
    public class SeatsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string strBusType = OBTSEnum.ToString(OBTSEnum.Types.BusType);
        private string strBrand = OBTSEnum.ToString(OBTSEnum.Types.Brand);

        // GET: api/Seats
        [ResponseType(typeof(Seat))]
        public async Task<IHttpActionResult> GetSeats()
        {
            return Ok((await (db.Seats).ToListAsync()).AsQueryable());
        }

        // GET: api/bus/1/seats
        [ResponseType(typeof(SeatDTO))]
        [Route("api/bus/{Id}/seats", Name = "GetSeatsByBus")]
        public async Task<IHttpActionResult> GetSeatsByBus(Guid Id)
        {
            var seats =await( from b in db.Seats
                        where b.BusId.Equals(Id)

                        select new SeatDTO()
                        {
                            SeatId = b.SeatId,
                            BusId = b.BusId,
                            Company = b._bus.Company,
                            BrandDesc = string.Empty,
                            BusTypeDesc = string.Empty,
                            SeatNo = b.SeatNo,
                            Bookable=b.Bookable,
                            Space=b.Space,
                            SpecialSeat=b.SpecialSeat,
                            Status=b.Status,
                            UpperLower=b.UpperLower,
                            Row =b.Row,
                            Col = b.Col
                        }).OrderBy(u => u.Row).ThenBy(u => u.Col).ToListAsync();

            
            return Ok(seats.AsQueryable());
        }

        // GET: api/Seats/5
        [ResponseType(typeof(Seat))]
        public async Task<IHttpActionResult> GetSeat(Guid id)
        {
            Seat seat = await db.Seats.FindAsync(id);
            if (seat == null)
            {
                return NotFound();
            }

            return Ok(seat);
        }

        // PUT: api/Seats/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSeat(Guid id, Seat seat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != seat.SeatId)
            {
                return BadRequest();
            }

            db.Entry(seat).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatExists(id))
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

        [ResponseType(typeof(OBTSResponse))]
        [Route("api/seats/BulkInsertSeats", Name = "BulkInsertSeats")]
        public async Task<IHttpActionResult> BulkInsertSeats(Seat[] seats)
        {
            OBTSResponse rep=new OBTSResponse();
            rep.Success = true;
            rep.Message = "";

            if (seats.Length > 0)
            {
                using (var context = new ApplicationDbContext())
                {
                    // delete existing records
                    context.Database.ExecuteSqlCommand("DELETE FROM Seats WHERE BusId = {0}", seats[0].BusId);
                }
            }

            foreach (Seat seat in seats)
            {
                seat.SeatId=Guid.NewGuid();
                db.Seats.Add(seat);
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                rep.Fail = false;
                rep.Message = ex.Message;
            }

            return Ok(rep);
        }
        /*
        [ResponseType(typeof(OBTSResponse))]
        [Route("api/seats/BulkUpdateSeats", Name = "BulkUpdateSeats")]
        public async Task<IHttpActionResult> BulkUpdateSeats(Seat[] seats)
        {
            
            OBTSResponse rep = new OBTSResponse();
            rep.Success = true;
            rep.Message = "";

            foreach (Seat seat in seats)
            {
                if (SeatExists(seat.SeatId))
                {
                    db.Entry(seat).State = EntityState.Modified;
                }
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                rep.Fail = false;
                rep.Message = ex.Message;
            }

            return Ok(rep);
        }
        */

        // POST: api/Seats
        [ResponseType(typeof(Seat))]
        public async Task<IHttpActionResult> PostSeat(Seat seat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            seat.SeatId = Guid.NewGuid();
            db.Seats.Add(seat);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SeatExists(seat.SeatId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = seat.SeatId }, seat);
        }

        // DELETE: api/Seats/5
        [ResponseType(typeof(Seat))]
        public async Task<IHttpActionResult> DeleteSeat(Guid id)
        {
            Seat seat = await db.Seats.FindAsync(id);
            if (seat == null)
            {
                return NotFound();
            }

            db.Seats.Remove(seat);
            await db.SaveChangesAsync();

            return Ok(seat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SeatExists(Guid id)
        {
            return db.Seats.Count(e => e.SeatId == id) > 0;
        }
    }
}