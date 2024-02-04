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
    public class StudentsController : ControllerBase
    {
        private readonly MasterContext _context;

        public StudentsController(MasterContext context)
        {
            _context = context;
        }

        // GET: api/Students
        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }*/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetStudents()
        {
            var students = await _context.Students.Select(s => new
            {
                s.IdStudenta,
                s.Ime,
                s.Prezime,
                brojIndeksa = $"{s.Smer}-{s.Broj}/{s.GodinaUpisa}"
            }).ToListAsync();
            return students;
        }

        [HttpGet("pretraga/{kriterijum}")]
        public async Task<ActionResult<IEnumerable<object>>> SearchStudents(string kriterijum)
        {
            string k = kriterijum.ToLower();
            var pretraga = await _context.Students
                .Where(s => s.Ime.ToLower().Contains(k) || s.Prezime.ToLower().Contains(k)
                || s.Smer.ToLower().Contains(k) || s.GodinaUpisa.ToLower().Contains(k)
                || s.Broj.ToString().ToLower().Contains(k))
                .Select(s => new
                {
                    s.IdStudenta,
                    s.Ime,
                    s.Prezime,
                    brojIndeksa = $"{s.Smer}-{s.Broj}/{s.GodinaUpisa}"
                })
                .ToListAsync();
            return pretraga;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.IdStudenta)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.IdStudenta)
            {
                return BadRequest();
            }
            Student s = await _context.Students.FindAsync(id);
            if (s == null)
            {
                return NotFound();
            }
            _context.Attach(s);
            s.Ime = student.Ime;
            s.Prezime = student.Prezime;
            _context.SaveChanges();
            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.IdStudenta }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.IdStudenta == id);
        }
    }
}
