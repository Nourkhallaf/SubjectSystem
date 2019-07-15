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
	[Authorize(Roles ="admin")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly EduModel _context;

        public InstructorsController(EduModel context)
        {
            _context = context;
        }

        // GET: api/Instructors
        [HttpGet]
        public IEnumerable<Instructor> GetInstructors()
        {
            return _context.Instructors.Include(i => i.User);
        }

        // GET: api/Instructors/5
        [HttpGet("{id}")]
        public IActionResult GetInstructor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instructor = _context.Instructors.Include(i=>i.User).SingleOrDefault(i=>i.Id==id);

            if (instructor == null)
            {
                return NotFound();
            }

            return Ok(instructor);
        }

        // PUT: api/Instructors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstructor([FromRoute] int id, [FromBody] Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != instructor.Id)
            {
                return BadRequest();
            }

            _context.Entry(instructor.User).State = EntityState.Modified;
			_context.Entry(instructor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(id))
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

        // POST: api/Instructors
        [HttpPost]
        public async Task<IActionResult> PostInstructor([FromBody] Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			_context.Users.Add(instructor.User);
            _context.Instructors.Add(instructor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InstructorExists(instructor.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInstructor", new { id = instructor.Id }, instructor);
        }

        // DELETE: api/Instructors/5
        [HttpDelete("{id}")]
        public IActionResult DeleteInstructor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			var instructor = _context.Instructors.Include(i => i.User).SingleOrDefault(i => i.Id == id);
			User user = instructor.User;
			if (instructor == null)
            {
                return NotFound();
            }

            _context.Instructors.Remove(instructor);
			_context.Users.Remove(user);
			_context.SaveChanges();

            return Ok(instructor);
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.Id == id);
        }
    }
}