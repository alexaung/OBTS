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
//using OBTS.API;
using OBTS.API.Models;
using System.Configuration;
using OBTS.API.Models.DTO;
using System.Linq.Expressions;

namespace OBTS.API.Controllers
{
    public class CodeTablesController : ApiController
    {
        private OBTSAPIContext db = new OBTSAPIContext();

        // Typed lambda expression for Select() method. 
        private static readonly Expression<Func<CodeTable, CodeValueDTO>> AsCodeValueDTO =
            a => new CodeValueDTO
            {
                KeyCode = a.KeyCode,
                Value = a.Value,
            };

        // Typed lambda expression for Select() method. 
        private static readonly Expression<Func<CodeTable, CodeTitleDTO>> AsCodeTitleDTO =
            a => new CodeTitleDTO
            {
                Title = a.Title,
            };
             
        // GET: api/CodeTables
        public IQueryable<CodeTable> GetCodeTables()
        {
            return db.CodeTables;
        }

        // GET: api/codetables/title/title-value
        [Route("api/codetables/title/{Title}", Name = "GetCodeByTitle")]
        public IQueryable<CodeValueDTO> GetCodeByTitle(string Title)
        {
            // City city = await db.Cities.FindAsync(id);
            return db.CodeTables
            .Where(b => b.Title == Title)
            .Select(AsCodeValueDTO);
        }

        // GET: api/codetables/titles
        [Route("api/codetables/titles", Name = "GetCodeTableTitles")]
        public IQueryable<CodeTitleDTO> GetCodeTableTitles()
        {
            // City city = await db.Cities.FindAsync(id);
            return db.CodeTables
            .Select(AsCodeTitleDTO).Distinct();
        }

        // GET: api/CodeTables/5
        [ResponseType(typeof(CodeTable))]
        public async Task<IHttpActionResult> GetCodeTable(Guid id)
        {
            CodeTable codeTable = await db.CodeTables.FindAsync(id);
            if (codeTable == null)
            {
                return NotFound();
            }

            return Ok(codeTable);
        }

        // PUT: api/CodeTables/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCodeTable(Guid id, CodeTable codeTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != codeTable.CodeTableId)
            {
                return BadRequest();
            }

            db.Entry(codeTable).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodeTableExists(id))
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

        // POST: api/CodeTables
        [ResponseType(typeof(CodeTable))]
        public async Task<IHttpActionResult> PostCodeTable(CodeTable codeTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CodeTables.Add(codeTable);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = codeTable.CodeTableId }, codeTable);
        }

        // DELETE: api/CodeTables/5
        [ResponseType(typeof(CodeTable))]
        public async Task<IHttpActionResult> DeleteCodeTable(Guid id)
        {
            CodeTable codeTable = await db.CodeTables.FindAsync(id);
            if (codeTable == null)
            {
                return NotFound();
            }

            db.CodeTables.Remove(codeTable);
            await db.SaveChangesAsync();

            return Ok(codeTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CodeTableExists(Guid id)
        {
            return db.CodeTables.Count(e => e.CodeTableId == id) > 0;
        }
    }
}