using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Models;
using WebAPICore.Model;

namespace WebAPICore.Controllers
{
	//[Route("api/auth")]
	[Route("api/auth")]
	[ApiController]
    public class AuthController : ControllerBase
    {
		private readonly EduModel _context;
		public AuthController(EduModel context)
		{
			_context = context;
		}
		[HttpPost, Route("login")]
		public IActionResult Login([FromBody]Login user)
		{
			if (user == null)
			{
				return BadRequest("Invalid client request");
			}

			User usr= _context.Users.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password);
			//if (user.UserName == "johndoe" && user.Password == "def@123")
			if (usr!=null)
			{
				var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
				var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
				//EncryptingCredentials encryptingCredentials = new EncryptingCredentials()
				//Claim claim= new Claim()
				//JwtHeader jwtHeader = new JwtHeader(signinCredentials);
				//JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(JwtHeader, JwtSecurityToken, String, String, String, String, String);
				var tokeOptions = new JwtSecurityToken(
					issuer: "http://localhost:63239",
					audience: "http://localhost:63239",
					claims: new List<Claim>() {new Claim(ClaimTypes.Role,usr.Role), new Claim("Id",usr.Id.ToString()), new Claim("Role", usr.Role) },
					expires: DateTime.Now.AddMinutes(5),
					signingCredentials: signinCredentials
				);

				var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
				return Ok(new { Token = tokenString });
			}
			else
			{
				return Unauthorized();
			}
		}
	}
}