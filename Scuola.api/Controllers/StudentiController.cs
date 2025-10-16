using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scuola.api.Data;
using Scuola.api.Models;

namespace Scuola.api.Controllers
{
    /// <summary>
    /// 🎓 Controller dedicato alla gestione degli studenti.
    /// 
    /// Questo controller consente di:
    /// - Visualizzare tutti gli studenti
    /// - Cercare uno studente per ID
    /// - Creare nuovi studenti
    /// - Aggiornare studenti esistenti
    /// - Eliminare studenti dal database
    /// 
    /// ⚠️ **Nota importante per gli studenti del corso:**
    /// Tutti gli endpoint sono a scopo didattico e mostrano come
    /// EF Core gestisce le operazioni CRUD con SQL Server.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentiController : ControllerBase
    {
        private readonly ScuolaDbContext _context;
        private readonly ILogger<StudentiController> _logger;

        public StudentiController(ScuolaDbContext context, ILogger<StudentiController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Restituisce la lista degli studenti presenti nel database.
        /// </summary>
        /// <returns></returns>
        // GET: api/Studenti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Studenti>>> GetStudentiList()
        {
            IEnumerable<Studenti> studenti = await _context.Studentis.ToListAsync();
            if (!studenti.Any())
            {
                return NotFound("Nessuno studente trovato nel db.");
            }

            return Ok(studenti);
        }

        // GET: api/Studenti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Studenti>> GetStudentiById(int id)
        {
            Studenti? studente = await _context.Studentis.FindAsync(id);

            if (studente == null)
            {
                return NotFound();
            }

            return Ok(studente);
        }

        // PUT: api/Studenti/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudenti(int id, Studenti studente)
        {
            if (id != studente.Id)
            {
                return BadRequest();
            }

            _context.Entry(studente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StudentiExists(id))
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

        // POST: api/Studenti
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Studenti>> PostStudenti(Studenti studente)
        {
            await _context.Studentis.AddAsync(studente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentiById), new { id = studente.Id }, studente);
        }

        // DELETE: api/Studenti/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudente(int id)
        {
            Studenti? studente = await _context.Studentis.FindAsync(id);
            if (studente == null)
            {
                return NotFound();
            }

            _context.Studentis.Remove(studente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> StudentiExists(int id)
        {
            return await _context.Studentis.AnyAsync(e => e.Id == id);
        }
    }
}
