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
    public class VotiController : ControllerBase
    {
        private readonly ScuolaDbContext _context;

        public VotiController(ScuolaDbContext context)
        {
            _context = context;
        }

        // GET: api/Voti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voti>>> GetVotis()
        {
            return await _context.Votis.ToListAsync();
        }

        // GET: api/Voti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voti>> GetVoti(int id)
        {
            var voti = await _context.Votis.FindAsync(id);

            if (voti == null)
            {
                return NotFound();
            }

            return voti;
        }

        // PUT: api/Voti/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoti(int id, Voti voti)
        {
            if (id != voti.Id)
            {
                return BadRequest();
            }

            _context.Entry(voti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VotiExists(id))
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

        // POST: api/Voti
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Voti>> PostVoti(Voti voti)
        {
            _context.Votis.Add(voti);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoti", new { id = voti.Id }, voti);
        }

        // DELETE: api/Voti/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoti(int id)
        {
            var voti = await _context.Votis.FindAsync(id);
            if (voti == null)
            {
                return NotFound();
            }

            _context.Votis.Remove(voti);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VotiExists(int id)
        {
            return _context.Votis.Any(e => e.Id == id);
        }
    }
}
