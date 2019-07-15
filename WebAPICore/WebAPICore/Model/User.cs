using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
	public class User
	{
		[Required]
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(20, MinimumLength = 2)]
		public string Fname { get; set; }
		[StringLength(20, MinimumLength = 2)]
		[Required]
		public string Lname { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public string Role { get; set; }
	}
}