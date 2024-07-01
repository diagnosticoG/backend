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
    public class PersonaController : ControllerBase
    {
        private readonly backendContext _context;

        public PersonaController(backendContext context)
        {
            _context = context;
        }

        // GET: api/Persona
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPersonas()
        {
            var personas = await _context.Persona
                .Select(p => new {
                    Id = p.Id,
                    Cedula = p.Cedula,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Fecha_Nacimiento = p.Fecha_Nacimiento,
                    Genero_Nombre = p.Genero.Nombre,
                    Genero_Id = p.Genero_Id,
                    Estado_Civil = p.Estado_Civil
                })
                .ToListAsync();

            return Ok(personas);
        }

        // GET: api/Persona/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await _context.Persona.FindAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }

        // POST: api/Persona
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(Persona persona)
        {
            _context.Persona.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersona", new { id = persona.Id }, persona);
        }

        // PUT: api/Persona/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, Persona persona)
        {
            if (id != persona.Id)
            {
                return BadRequest();
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
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

        // DELETE: api/Persona/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Persona.Remove(persona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaExists(int id)
        {
            return _context.Persona.Any(e => e.Id == id);
        }
    }
}
