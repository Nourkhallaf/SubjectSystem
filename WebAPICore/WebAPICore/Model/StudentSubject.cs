using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
	public class StudentSubject
	{
		[ForeignKey("Subject")]
		[Key]
		[Column(Order = 1)]
		public int Subj_Id { get; set; }
		[ForeignKey("Student")]
		[Key]
		[Column(Order = 2)]
		public int Id { get; set; }

		public int? Grade { get; set; }
		public Student Student { get; set; }
		public Subject Subject { get; set; }
	}
}