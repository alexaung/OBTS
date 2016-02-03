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
//using OBTSAPI.DbContexts;
using OBTS.API.Models;
using System.Configuration;
using OBTS.API.Models.DTO;
using System.Linq.Expressions;
using AutoMapper;

namespace OBTS.API.Controllers
{
    [Authorize]
    public class BusesController : ApiController
    {
        private ApplicationDbContext  db = new ApplicationDbContext ();
        private string strBusType = OBTSEnum.ToString(OBTSEnum.Types.BusType);
        private string strBrand = OBTSEnum.ToString(OBTSEnum.Types.Brand);

        // Typed lambda expression for Select() method. 
        private static readonly Expression<Func<CodeTable, CodeValueDTO>> AsCodeValueDTO =
            a => new CodeValueDTO
            {
                KeyCode = a.KeyCode,
                Value = a.Value,
            };

        // GET: api/Buses
        [ResponseType(typeof(BusDTO))]
        public async Task<IHttpActionResult> GetBuses()
        {
           var buses = await(from b in db.Buses
                        join ct in db.CodeTables on b.BusType equals  ct.KeyCode
                        where ct.Title.Equals(strBusType) 
                        join ct1 in db.CodeTables on b.Brand equals ct1.KeyCode
                        where ct1.Title.Equals(strBrand)
                        join op in db.Operators on b.OperatorId equals op.OperatorId
                       select new BusDTO()
                         {
                                BusId = b.BusId,
                                Company=b.Company,
                                Brand =b.Brand,
                                BrandDesc= ct1.Value,
                                BusType=b.BusType,
                                BusTypeDesc=ct.Value,
                                RegistrationNo=b.RegistrationNo,
                                PermitNumber =b.PermitNumber,
                                PermitRenewDate =b.PermitRenewDate,
                                InsurancePolicyNumber =b.InsurancePolicyNumber,
                                InsuranceCompany =b.InsuranceCompany,
                                InsuranceValidFrom =b.InsuranceValidFrom,
                                InsuranceValidTo =b.InsuranceValidTo,
                                VechiclePhoneNo =b.VechiclePhoneNo,
                                DriverName =b.DriverName,
                                Description =b.Description,
                                Status =b.Status,
                                OperatorId=b.OperatorId,
                                OperatorCompany= op.Company,
                                NoOfSeat = 0

                         }).ToListAsync();
            return Ok(buses.AsQueryable());
        }

        // GET: api/Buses
        [ResponseType(typeof(BusDTO))]
        [Route("api/buses/operator/{Id}", Name = "GetBusesByOperator")]
        public async Task<IHttpActionResult> GetBusesByOperator(Guid Id)
        {
            
            var buses = await (from b in db.Buses
                        join ct in db.CodeTables on b.BusType equals ct.KeyCode
                        where ct.Title.Equals(strBusType) && b.OperatorId.Equals(Id)
                        join ct1 in db.CodeTables on b.Brand equals ct1.KeyCode
                        where ct1.Title.Equals(strBrand)
                        join op in db.Operators on b.OperatorId equals op.OperatorId

                        select new BusDTO()
                        {
                            BusId = b.BusId,
                            Company = b.Company,
                            Brand = b.Brand,
                            BrandDesc = ct1.Value,
                            BusType = b.BusType,
                            BusTypeDesc = ct.Value,
                            RegistrationNo = b.RegistrationNo,
                            PermitNumber = b.PermitNumber,
                            PermitRenewDate = b.PermitRenewDate,
                            InsurancePolicyNumber = b.InsurancePolicyNumber,
                            InsuranceCompany = b.InsuranceCompany,
                            InsuranceValidFrom = b.InsuranceValidFrom,
                            InsuranceValidTo = b.InsuranceValidTo,
                            VechiclePhoneNo = b.VechiclePhoneNo,
                            DriverName = b.DriverName,
                            Description = b.Description,
                            Status = b.Status,
                            OperatorId = b.OperatorId,
                            OperatorCompany = op.Company

                        }).ToListAsync();
            return Ok(buses.AsQueryable());
        }

        // GET: api/Buses/5
        [ResponseType(typeof(BusDTO))]
        [Route("api/bus/{Id}", Name = "GetBus")]
        public async Task<IHttpActionResult> GetBus(Guid id)
        {
            var bus = await( from b in db.Buses
                        join ct in db.CodeTables on b.BusType equals ct.KeyCode
                        where ct.Title.Equals(strBusType) && b.BusId.Equals(id)
                        join ct1 in db.CodeTables on b.Brand equals ct1.KeyCode
                        where ct1.Title.Equals(strBrand)
                        join op in db.Operators on b.OperatorId equals op.OperatorId

                        select new BusDTO()
                        {
                            BusId = b.BusId,
                            Company = b.Company,
                            Brand = b.Brand,
                            BrandDesc = ct1.Value,
                            BusType = b.BusType,
                            BusTypeDesc = ct.Value,
                            RegistrationNo = b.RegistrationNo,
                            PermitNumber = b.PermitNumber,
                            PermitRenewDate = b.PermitRenewDate,
                            InsurancePolicyNumber = b.InsurancePolicyNumber,
                            InsuranceCompany = b.InsuranceCompany,
                            InsuranceValidFrom = b.InsuranceValidFrom,
                            InsuranceValidTo = b.InsuranceValidTo,
                            VechiclePhoneNo = b.VechiclePhoneNo,
                            DriverName = b.DriverName,
                            Description = b.Description,
                            Status = b.Status,
                            OperatorId = b.OperatorId,
                            OperatorCompany = op.Company

                        }).SingleOrDefaultAsync(b => b.BusId == id);
            //BusDTO bus = await buses.SingleOrDefaultAsync(b => b.BusId == id);
            if (bus == null)
            {
                return NotFound();
            }

            return Ok(bus);
            /*
            Bus bus = await db.Buses.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }

            return Ok(bus);*/
        }

        // PUT: api/Buses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBus(Guid id, Bus bus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bus.BusId)
            {
                return BadRequest();
            }

            //Bus dbBus = await db.Buses.FindAsync(bus.BusId);
            //bus.CreatedBy = dbBus.CreatedBy;
            //bus.CreatedUtc = dbBus.CreatedUtc;

            db.Entry(bus).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusExists(id))
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

        // POST: api/Buses
        [ResponseType(typeof(Bus))]
        public async Task<IHttpActionResult> PostBus(Bus bus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bus.BusId = Guid.NewGuid();
            db.Buses.Add(bus);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bus.BusId }, bus);
        }

        // DELETE: api/Buses/5
        [ResponseType(typeof(Bus))]
        public async Task<IHttpActionResult> DeleteBus(int id)
        {
            Bus bus = await db.Buses.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }

            db.Buses.Remove(bus);
            await db.SaveChangesAsync();

            return Ok(bus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusExists(Guid id)
        {
            return db.Buses.Count(e => e.BusId == id) > 0;
        }
    }
}