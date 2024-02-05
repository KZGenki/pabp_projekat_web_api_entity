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
    public class Prijava_brojIndeksaController : ControllerBase
    {
        private readonly MasterContext _context;

        public Prijava_brojIndeksaController(MasterContext context)
        {
            _context = context;
        }

        // GET: api/Prijava_brojIndeksa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prijava_brojIndeksa>>> GetPrijavas()
        {
            return await _context.Prijava_brojIndeksa.ToListAsync();
        }

        [HttpGet("student/{id}")]
        public async Task<ActionResult<IEnumerable<Prijava_brojIndeksa>>> GetPrijava_brojIndeksaZaStudenta(int id)
        {
            var prijava_brojIndeksa = await _context.Prijava_brojIndeksa.Where(p => p.IdStudenta == id).ToListAsync();

            if (prijava_brojIndeksa == null)
            {
                return NotFound();
            }

            return prijava_brojIndeksa;
        }
        [HttpGet("ispit/{id}")]
        public async Task<ActionResult<IEnumerable<Prijava_brojIndeksa>>> GetPrijava_brojIndeksaZaIspit(int id)
        {
            var prijava_brojIndeksa = await _context.Prijava_brojIndeksa.Where(p => p.IdIspita == id).ToListAsync();

            if (prijava_brojIndeksa == null)
            {
                return NotFound();
            }

            return prijava_brojIndeksa;
        }


        // GET: api/Prijava_brojIndeksa/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Prijava_brojIndeksa>> GetPrijava_brojIndeksa(int id)
        {
            var prijava_brojIndeksa = await _context.Prijavas.FindAsync(id);

            if (prijava_brojIndeksa == null)
            {
                return NotFound();
            }

            return prijava_brojIndeksa;
        }*/

        // PUT: api/Prijava_brojIndeksa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutPrijava_brojIndeksa(int id, Prijava_brojIndeksa prijava_brojIndeksa)
        {
            if (id != prijava_brojIndeksa.IdStudenta)
            {
                return BadRequest();
            }

            _context.Entry(prijava_brojIndeksa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Prijava_brojIndeksaExists(id))
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

        // POST: api/Prijava_brojIndeksa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prijava_brojIndeksa>> PostPrijava_brojIndeksa(PrijavaIspita prijava)
        {
            Prijava_brojIndeksa prijava_brojIndeksa = new Prijava_brojIndeksa();
            prijava_brojIndeksa.IdIspita = prijava.IdIspita;
            prijava_brojIndeksa.IdStudenta = prijava.IdStudenta;
            _context.Prijava_brojIndeksa.Add(prijava_brojIndeksa);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Prijava_brojIndeksaExists(prijava_brojIndeksa.IdStudenta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // DELETE: api/Prijava_brojIndeksa/5
        [HttpDelete("{idStudenta}/{idIspita}")]
        public async Task<IActionResult> DeletePrijava_brojIndeksa(int idStudenta, int idIspita)
        {
            var prijava_brojIndeksa = await _context.Prijava_brojIndeksa.Where(p => p.IdIspita == idIspita && p.IdStudenta == idStudenta).SingleOrDefaultAsync();
            if (prijava_brojIndeksa == null)
            {
                return NotFound();
            }

            _context.Prijava_brojIndeksa.Remove(prijava_brojIndeksa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Prijava_brojIndeksaExists(int id)
        {
            return _context.Prijava_brojIndeksa.Any(e => e.IdStudenta == id);
        }
    }
}
