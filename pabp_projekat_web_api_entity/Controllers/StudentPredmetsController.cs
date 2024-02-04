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
    public class StudentPredmetsController : ControllerBase
    {
        private readonly MasterContext _context;

        public StudentPredmetsController(MasterContext context)
        {
            _context = context;
        }

        // GET: api/StudentPredmets
        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<StudentPredmet>>> GetStudentPredmets()
        {
            return await _context.StudentPredmets.ToListAsync();
        }*/

        // GET: api/StudentPredmets/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<StudentPredmet>> GetStudentPredmet(int id)
        {
            var studentPredmet = await _context.StudentPredmets.FindAsync(id);

            if (studentPredmet == null)
            {
                return NotFound();
            }

            return studentPredmet;
        }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Predmet>>> GetPredmetsOfStudent(short id)
        {
            var predmetsOfStudentIds = await _context.StudentPredmets
                .Where(sp => sp.IdStudenta == id)
                .Select(sp => sp.IdPredmeta)
                .ToListAsync();

            if (predmetsOfStudentIds == null)
            {
                return NotFound();
            }
            var predmeti = await _context.Predmets
                .Where(p => predmetsOfStudentIds.Contains(p.IdPredmeta))
                .ToListAsync();

            return predmeti;
        }
        [HttpGet("predmeti")]
        public async Task<ActionResult<IEnumerable<Predmet>>> GetPredmets()
        {
            var predmets = await _context.Predmets.ToListAsync();

            if (predmets == null)
            {
                return NotFound();
            }
            return predmets;
        }
        [HttpGet("nepolozeni/{id}")]
        public async Task<ActionResult<IEnumerable<Predmet>>> GetUnfinishedPredmetsOfStudent(int id)
        {
            var predmetsOfStudentIds = await _context.StudentPredmets
                .Where(sp => sp.IdStudenta == id)
                .Select(sp => sp.IdPredmeta)
                .ToListAsync();
            if (predmetsOfStudentIds == null)
            {
                return NotFound();
            }
            var zapisniks = await _context.Zapisniks
                .Where(z => z.IdStudenta == id && z.Ocena >= 6)
                .Select(z => z.IdIspita)
                .ToListAsync();
            var polozeniPredmetiIds = await _context.Ispits
                .Where(i => zapisniks.Contains(i.IdIspita))
                .Select(i => i.IdPredmeta)
                .ToListAsync();

            var predmeti = await _context.Predmets.Where(p => predmetsOfStudentIds.Contains(p.IdPredmeta) && !polozeniPredmetiIds.Contains(p.IdPredmeta)).ToListAsync();

            return predmeti;
        }



        // PUT: api/StudentPredmets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutStudentPredmet(int id, StudentPredmet studentPredmet)
        {
            if (id != studentPredmet.IdStudenta)
            {
                return BadRequest();
            }

            _context.Entry(studentPredmet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentPredmetExists(id))
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

        // POST: api/StudentPredmets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentPredmet>> PostStudentPredmet(NewStudentPredmet sp)
        {
            StudentPredmet studentPredmet = new StudentPredmet();
            studentPredmet.IdPredmeta = sp.IdPredmeta;
            studentPredmet.IdStudenta = sp.IdStudenta;
            studentPredmet.SkolskaGodina = sp.SkolskaGodina;
            _context.StudentPredmets.Add(studentPredmet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentPredmetExists(studentPredmet.IdStudenta))
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

        // DELETE: api/StudentPredmets/5
        [HttpDelete("{idStudenta}/{idPredmeta}")]
        public async Task<IActionResult> DeleteStudentPredmet(short idStudenta, short idPredmeta)
        {
            var zapisniks = await _context.Zapisniks
                .Where(z => z.IdStudenta == idStudenta && z.Ocena >= 6)
                .Select(z => z.IdIspita)
                .ToListAsync();
            var polozeniPredmetiIds = await _context.Ispits
                .Where(i => zapisniks.Contains((short)i.IdIspita))
                .Select(i => i.IdPredmeta)
                .ToListAsync();

            var studentPredmet = await _context.StudentPredmets
                .Where(sp => sp.IdStudenta == idStudenta && sp.IdPredmeta == idPredmeta && !polozeniPredmetiIds.Contains(sp.IdPredmeta))
                .FirstOrDefaultAsync();
            if (studentPredmet == null && zapisniks.Count() > 0)
            {
                return BadRequest("Predmet je vec polozen, ne moze se ukloniti");
            }

            _context.StudentPredmets.Remove(studentPredmet);
            await _context.SaveChangesAsync();
            return NoContent();

            //return Ok(studentPredmet);
        }

        private bool StudentPredmetExists(int id)
        {
            return _context.StudentPredmets.Any(e => e.IdStudenta == id);
        }
    }
}
