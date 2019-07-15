using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
	public class Instructor
	{
		[Key]
		[ForeignKey("User")]
		public int Id { get; set; }
		[Required]
		public int Salary { get; set; }
		public List<Subject> Subjects { get; set; }
		public User User { get; set; }
	}
}