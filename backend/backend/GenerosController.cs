using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly backendContext _context;

        public GenerosController(backendContext context)
        {
            _context = context;
        }

        // GET: api/Generos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
        {
            return await _context.Genero.ToListAsync();
        }

        // GET: api/Generos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id)
        {
            var genero = await _context.Genero.FindAsync(id);

            if (genero == null)
            {
                return NotFound();
            }

            return genero;
        }

        // POST: api/Generos
        [HttpPost]
        public async Task<ActionResult<Genero>> PostGenero(Genero genero)
        {
            _context.Genero.Add(genero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenero", new { id = genero.Id }, genero);
        }

        // PUT: api/Generos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenero(int id, Genero genero)
        {
            if (id != genero.Id)
            {
                return BadRequest();
            }

            _context.Entry(genero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroExists(id))
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

        // DELETE: api/Generos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            var genero = await _context.Genero.FindAsync(id);
            if (genero == null)
            {
                return NotFound();
            }

            _context.Genero.Remove(genero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeneroExists(int id)
        {
            return _context.Genero.Any(e => e.Id == id);
        }
    }
}
