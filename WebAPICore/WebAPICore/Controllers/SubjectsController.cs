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
	[Authorize(Roles = "instructor, admin")]
	[ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly EduModel _context;

        public SubjectsController(EduModel context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public IEnumerable<Subject> GetSubjects()
        {
			return _context.Subjects.Include(s => s.Instructor.User);
        }

        // GET: api/Subjects/5 //Get Subjects By Instructor ID
        [HttpGet("{id}")]
        public IActionResult GetSubject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subjects = _context.Subjects.Where(s=>s.Id==id).ToList();

            if (subjects == null)
            {
                return NotFound();
            }

            return Ok(subjects);
        }

        // PUT: api/Subjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject([FromRoute] int id, [FromBody] Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subject.Subj_Id)
            {
                return BadRequest();
            }

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // POST: api/Subjects
        [HttpPost]
        public IActionResult PostSubject([FromBody] Subject subject)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
			if(subject.Subj_Id!=0)
			{
				subject.Subj_Id = 0;
			}
			subject.Instructor.User = null;
			subject.Instructor = null;
            _context.Subjects.Add(subject);
            _context.SaveChanges();

            return CreatedAtAction("GetSubject", new { id = subject.Subj_Id }, subject);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return Ok(subject);
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.Subj_Id == id);
        }
    }
}