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
    public class ZapisniksController : ControllerBase
    {
        private readonly MasterContext _context;

        public ZapisniksController(MasterContext context)
        {
            _context = context;
        }

        // GET: api/Zapisniks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zapisnik>>> GetZapisniks()
        {
            return await _context.Zapisniks.ToListAsync();
        }

        // GET: api/Zapisniks/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Zapisnik>> GetZapisnik(int id)
        {
            var zapisnik = await _context.Zapisniks.FindAsync(id);

            if (zapisnik == null)
            {
                return NotFound();
            }

            return zapisnik;
        }*/
        [HttpGet("{idStudenta}/{idIspita}")]
        public async Task<ActionResult<Zapisnik>> GetZapisnik(short idStudenta, short idIspita)
        {
            var zapisnik = await _context.Zapisniks
                .Where(z => z.IdStudenta == idStudenta && z.IdIspita == idIspita)
                .FirstOrDefaultAsync();

            if (zapisnik == null)
            {
                return NotFound();
            }

            return zapisnik;
        }
        [HttpGet("Student/{idStudenta}")]
        public async Task<ActionResult<IEnumerable<Zapisnik>>> GetZapisnikStudent(short idStudenta)
        {
            var zapisniks = await _context.Zapisniks
                .Where(z => z.IdStudenta == idStudenta)
                .ToListAsync();

            if (zapisniks == null)
            {
                return NotFound();
            }

            return zapisniks;
        }
        [HttpGet("Ispit/{idIspita}")]
        public async Task<ActionResult<IEnumerable<Zapisnik>>> GetZapisnikIspit(short idIspita)
        {
            var zapisniks = await _context.Zapisniks
                .Where(z => z.IdIspita == idIspita)
                .ToListAsync();

            if (zapisniks == null)
            {
                return NotFound();
            }

            return zapisniks;
        }
        [HttpGet("Polozeni/{id}")]
        public async Task<ActionResult<object>> GetPolozeni(short id)
        {
            var zapisnik = await _context.Zapisniks
                .Where(z => z.IdStudenta == id && z.Ocena > 5)
                .ToListAsync();

            List<int> ispitiId = new List<int>();
            List<object> ocene = new List<object>();

            double suma = 0;
            foreach (var item in zapisnik)
            {
                ispitiId.Add(item.IdIspita);
                ocene.Add(new { ocena = item.Ocena, id = item.IdIspita });
                suma += item.Ocena;
            }
            double prosek = suma / ispitiId.Count();

            var ispiti = await _context.Ispits
                .Where(i => ispitiId.Contains(i.IdIspita))
                .ToListAsync();
            var predmetiId = ispiti
                .Select(i => i.IdPredmeta);

            var Predmeti = await _context.Predmets
                .Where(p => predmetiId.Contains(p.IdPredmeta))
                .ToListAsync();



            return Ok(new
            {
                prosecnaOcena = prosek,
                polozeniPredmeti = Predmeti
            });
        }

        // PUT: api/Zapisniks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutZapisnik(int id, Zapisnik zapisnik)
        {
            if (id != zapisnik.IdStudenta)
            {
                return BadRequest();
            }

            _context.Entry(zapisnik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZapisnikExists(id))
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

        // POST: api/Zapisniks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Zapisnik>> PostZapisnik(NewZapisnik zap)
        {
            if(!(zap.Ocena>5 && zap.Ocena <= 10))
            {
                return BadRequest("Ocena izvan opsega [6,10]");
            }
            Zapisnik zapisnik = new Zapisnik();
            zapisnik.IdStudenta = zap.IdStudenta;
            zapisnik.IdIspita = zap.IdIspita;
            zapisnik.Ocena = zap.Ocena;
            zapisnik.Bodovi = zap.Bodovi;
            _context.Zapisniks.Add(zapisnik);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ZapisnikExists(zapisnik.IdStudenta, zapisnik.IdIspita))
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

        // DELETE: api/Zapisniks/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZapisnik(int id)
        {
            var zapisnik = await _context.Zapisniks.FindAsync(id);
            if (zapisnik == null)
            {
                return NotFound();
            }

            _context.Zapisniks.Remove(zapisnik);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool ZapisnikExists(int idStudenta, int idIspita)
        {
            return _context.Zapisniks.Any(e => e.IdStudenta == idStudenta && e.IdIspita == idIspita);
        }
    }
}
