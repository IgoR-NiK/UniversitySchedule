using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using Repository.Interfaces;
using UniversitySchedule.Authentication;

namespace UniversitySchedule.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		IUserRepository UserRepository { get; }

		public LoginController(IUserRepository userRepository)
		{
			UserRepository = userRepository;
		}


		[HttpPost]
		public async Task<IActionResult> Post([FromBody] string login, [FromBody] string password)
		{
			var user = await UserRepository.GetUserAsync(login, password);

			if (user == null)
			{
				return new BadRequestObjectResult("Invalid username or password.");
			}

			var claims = new List<Claim>()
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
			};
			var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			
			var now = DateTime.UtcNow;			
			var jwt = new JwtSecurityToken(
				issuer: AuthOptions.Issuer,
				audience: AuthOptions.Audience,
				notBefore: now,
				claims: claimsIdentity.Claims,
				expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LifeTime)),
				signingCredentials: new SigningCredentials(AuthOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			return new OkObjectResult(new
			{
				Token = encodedJwt,
				FirstName = user.FirstName,
				SecondName = user.SecondName
			});
		}
	}
}
