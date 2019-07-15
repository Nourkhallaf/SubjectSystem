using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
	public class Subject
	{
		[Key]
		public int Subj_Id { get; set; }
		[Required]
		[StringLength(30,MinimumLength =1)]
		public string Name { get; set; }
		[ForeignKey("Instructor")]
		public int Id { get; set; }
		public Instructor Instructor { get; set; }
		public List<StudentSubject> StudentsSubjects { get; set; }
	}
}