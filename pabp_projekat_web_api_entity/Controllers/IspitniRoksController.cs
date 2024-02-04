using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pabp_projekat_web_api_entity.Models;

namespace pabp_projekat_web_api_entity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IspitniRoksController : ControllerBase
    {
        private readonly MasterContext _context;

        public IspitniRoksController(MasterContext context)
        {
            _context = context;
        }

        // GET: api/IspitniRoks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IspitniRok>>> GetIspitniRoks()
        {
            return await _context.IspitniRoks.ToListAsync();
        }

        // GET: api/IspitniRoks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IspitniRok>> GetIspitniRok(int id)
        {
            var ispitniRok = await _context.IspitniRoks.FindAsync(id);

            if (ispitniRok == null)
            {
                return NotFound();
            }

            return ispitniRok;
        }

        // PUT: api/IspitniRoks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIspitniRok(int id, IspitniRok ispitniRok)
        {
            if (id != ispitniRok.IdRoka)
            {
                return BadRequest();
            }

            _context.Entry(ispitniRok).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IspitniRokExists(id))
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

        // POST: api/IspitniRoks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IspitniRok>> PostIspitniRok(IspitniRok ispitniRok)
        {
            _context.IspitniRoks.Add(ispitniRok);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIspitniRok", new { id = ispitniRok.IdRoka }, ispitniRok);
        }

        // DELETE: api/IspitniRoks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIspitniRok(int id)
        {
            var ispitniRok = await _context.IspitniRoks.FindAsync(id);
            if (ispitniRok == null)
            {
                return NotFound();
            }

            _context.IspitniRoks.Remove(ispitniRok);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IspitniRokExists(int id)
        {
            return _context.IspitniRoks.Any(e => e.IdRoka == id);
        }
    }
}
