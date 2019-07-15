using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPICore.Model;

namespace WebAPICore.Controllers
{
    [Route("api/[controller]")]
	[Authorize(Roles ="instructor, admin")]
	[ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly EduModel _context;

        public StudentsController(EduModel context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            return _context.Students.Include(s=>s.User);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public IActionResult GetStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = _context.Students.Include(a => a.User).SingleOrDefault(a => a.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent([FromRoute] int id, [FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student.User).State = EntityState.Modified;
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
        }

        // POST: api/Students
        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(student.User);
            _context.Students.Add(student);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentExists(student.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			var student = _context.Students.Include(s => s.User).SingleOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
			var studentsSubjects= _context.StudentsSubjects.Where(s => s.Id == id).ToList();
			User user = student.User;
			_context.StudentsSubjects.RemoveRange(studentsSubjects);
            _context.Students.Remove(student);
			_context.Users.Remove(user);
			_context.SaveChanges();

            return Ok(student);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}