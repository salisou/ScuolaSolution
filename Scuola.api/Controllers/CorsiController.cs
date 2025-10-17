using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scuola.api.Data;
using Scuola.api.Models;

namespace Scuola.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorsiController : ControllerBase
    {
        private readonly ScuolaDbContext _context;
        private readonly ILogger<CorsiController> _logger;

        public CorsiController(ScuolaDbContext context, ILogger<CorsiController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Corsi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Corsi>>> GetAllCorsi()
        {
            var corsi = await _context.Corsis.Include(c => c.Docente)
                .Select(c => new
                {
                    c.Id,
                    c.NomeCorso,
                    c.Descrizione,
                    Docente = c.Docente != null ? $"{c.Docente.Nome} {c.Docente.Cognome}" : "Non assegnato"
                })
                .ToListAsync();

            return Ok(corsi);
        }

        // GET: api/Corsi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Corsi>> GetCorsoById(int id)
        {
            var corsi = await _context.Corsis.FindAsync(id);

            if (corsi == null)
            {
                return NotFound();
            }

            return corsi;
        }

        // PUT: api/Corsi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorsi(int id, Corsi corsi)
        {
            if (id != corsi.Id)
            {
                return BadRequest();
            }

            _context.Entry(corsi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorsiExists(id))
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

        // POST: api/Corsi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Corsi>> PostCorsi(Corsi corsi)
        {
            _context.Corsis.Add(corsi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCorsi", new { id = corsi.Id }, corsi);
        }

        // DELETE: api/Corsi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorsi(int id)
        {
            var corsi = await _context.Corsis.FindAsync(id);
            if (corsi == null)
            {
                return NotFound();
            }

            _context.Corsis.Remove(corsi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CorsiExists(int id)
        {
            return _context.Corsis.Any(e => e.Id == id);
        }
    }
}
