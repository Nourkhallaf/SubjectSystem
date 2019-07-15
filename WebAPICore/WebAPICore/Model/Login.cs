using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
	public class Login
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}