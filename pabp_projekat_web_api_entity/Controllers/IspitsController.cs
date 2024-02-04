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
    public class IspitsController : ControllerBase
    {
        private readonly MasterContext _context;

        public IspitsController(MasterContext context)
        {
            _context = context;
        }

        // GET: api/Ispits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ispit>>> GetIspits()
        {
            return await _context.Ispits.ToListAsync();
        }

        // GET: api/Ispits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ispit>> GetIspit(int id)
        {
            var ispit = await _context.Ispits.FindAsync(id);

            if (ispit == null)
            {
                return NotFound();
            }

            return ispit;
        }

        [HttpGet("predmet/{id}")]
        public async Task<ActionResult<IEnumerable<Ispit>>> GetIspitPredmeta(int id)
        {
            var ispit = await _context.Ispits.Where(i => i.IdPredmeta == id).ToListAsync();

            if (ispit == null)
            {
                return NotFound();
            }
            return ispit;
        }

        // PUT: api/Ispits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutIspit(int id, Ispit ispit)
        {
            if (id != ispit.IdIspita)
            {
                return BadRequest();
            }

            _context.Entry(ispit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IspitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Ispits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<Ispit>> PostIspit(Ispit ispit)
        {
            _context.Ispits.Add(ispit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIspit", new { id = ispit.IdIspita }, ispit);
        }*/

        // DELETE: api/Ispits/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIspit(int id)
        {
            var ispit = await _context.Ispits.FindAsync(id);
            if (ispit == null)
            {
                return NotFound();
            }

            _context.Ispits.Remove(ispit);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool IspitExists(int id)
        {
            return _context.Ispits.Any(e => e.IdIspita == id);
        }
    }
}
