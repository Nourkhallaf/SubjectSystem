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
    public class StudentSubjectsController : ControllerBase
    {
        private readonly EduModel _context;

        public StudentSubjectsController(EduModel context)
        {
            _context = context;
        }

		//GET: api/StudentSubjects
	 //  [HttpGet]
		//public IEnumerable<StudentSubject> GetStudentsSubjects()
		//{
		//	return _context.StudentsSubjects.Include(s => s.Student.User).Include(s => s.Subject.Instructor.User);
		//}

		// GET: api/StudentSubjects?Id=5; Subj_Id=3
		[HttpGet]
        public IActionResult GetStudentSubject([FromQuery] int Id,int Subj_Id, int Grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			var studentSubject = _context.StudentsSubjects.Include(s=>s.Subject.Instructor.User).Include(s=>s.Student.User).SingleOrDefault(s => s.Subj_Id == Subj_Id && s.Id==Id);
			
            if (studentSubject == null)
            {
                return NotFound();
            }
			studentSubject.Grade = Grade;
			_context.SaveChanges();
			return Ok(studentSubject);
        }

		[HttpGet("{id}")]//, Route("subj")]
		public IActionResult GetStudentOfSubject([FromRoute] int id, [FromQuery(Name = "instId")] int instId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			
			var studentSubjects = _context.StudentsSubjects.Include(s => s.Student.User).Where(s => s.Subj_Id == id && s.Subject.Id==instId).ToList();

			if (studentSubjects == null)
			{
				return NotFound();
			}

			return Ok(studentSubjects);
		}

		// PUT: api/StudentSubjects/5
		[HttpPut]
        public async Task<IActionResult> PutStudentSubject([FromQuery] int Id, int Subj_Id, [FromBody] StudentSubject studentSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Subj_Id != studentSubject.Subj_Id && Id!= studentSubject.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentSubjectExists(Subj_Id,Id))
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

        // POST: api/StudentSubjects
        [HttpPost]
        public IActionResult PostStudentSubject([FromQuery] int Id, int Subj_Id)
        {
			
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			StudentSubject studentSubject = new StudentSubject();
			studentSubject.Id = Id;
			studentSubject.Subj_Id = Subj_Id;
            _context.StudentsSubjects.Add(studentSubject);
            try
            {
                 _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StudentSubjectExists(studentSubject.Subj_Id,studentSubject.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudentSubject", new { id = studentSubject.Id }, studentSubject);
        }

        // DELETE: api/StudentSubjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentSubject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studentSubject = await _context.StudentsSubjects.FindAsync(id);
            if (studentSubject == null)
            {
                return NotFound();
            }

            _context.StudentsSubjects.Remove(studentSubject);
            await _context.SaveChangesAsync();

            return Ok(studentSubject);
        }

        private bool StudentSubjectExists(int subjId, int Id)
        {
            return _context.StudentsSubjects.Count(e => e.Subj_Id == subjId && e.Id== Id)> 0;
        }
    }
}