using api2.Context;
using api2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoupasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoupasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Roupas>>> GetRoupas()
        {
            return await _context.Roupas.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Roupas>> GetRoupas(int id)
        {
            var roupas = await _context.Roupas.FindAsync(id);
            if (roupas == null)
            {
                return NotFound();
            }
            return roupas;
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> PutRoupas(int id, Roupas roupas)
        {
            {
                if (id != roupas.Id)
                {
                    return BadRequest();
                }
                _context.Entry(roupas).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoupasExists(id))
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
        }
        [HttpPost]
        public async Task<ActionResult<Roupas>> PostRoupas(Roupas roupas)
        {
            _context.Roupas.Add(roupas);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRoupas", new { id = roupas.Id }, roupas);

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteRoupas(int id)
        {
            var roupas = await _context.Roupas.FindAsync(id);
            if (roupas == null)
            {
                return NotFound();
            }
            _context.Roupas.Remove(roupas);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool RoupasExists(int id)
        {
            return _context.Roupas.Any(e => e.Id == id);
        }
    }
}
