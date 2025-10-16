using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scuola.api.Data;
using Scuola.api.Models;

namespace Scuola.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocentiController : ControllerBase
    {
        private readonly ScuolaDbContext _context;

        public DocentiController(ScuolaDbContext context)
        {
            _context = context;
        }

        // GET: api/Docenti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Docenti>>> GetDocentis()
        {
            return await _context.Docentis.ToListAsync();
        }

        // GET: api/Docenti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Docenti>> GetDocenti(int id)
        {
            var docenti = await _context.Docentis.FindAsync(id);

            if (docenti == null)
            {
                return NotFound();
            }

            return docenti;
        }

        // PUT: api/Docenti/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocenti(int id, Docenti docenti)
        {
            if (id != docenti.Id)
            {
                return BadRequest();
            }

            _context.Entry(docenti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocentiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Docenti
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Docenti>> PostDocenti(Docenti docenti)
        {
            _context.Docentis.Add(docenti);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocenti", new { id = docenti.Id }, docenti);
        }

        // DELETE: api/Docenti/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocenti(int id)
        {
            var docenti = await _context.Docentis.FindAsync(id);
            if (docenti == null)
            {
                return NotFound();
            }

            _context.Docentis.Remove(docenti);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocentiExists(int id)
        {
            return _context.Docentis.Any(e => e.Id == id);
        }
    }
}
