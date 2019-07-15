using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
	public class Student
	{
		[Key]
		[ForeignKey("User")]
		public int Id { get; set; }
		[Required]
		[Range(20,35)]
		public int Age { get; set; }
		public List<StudentSubject> StudentsSubjects { get; set; }
		public User User { get; set; }
	}
}