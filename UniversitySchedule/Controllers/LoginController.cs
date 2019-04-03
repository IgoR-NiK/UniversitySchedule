using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using Repository.Interfaces;
using UniversitySchedule.Authentication;
using UniversitySchedule.Filters;
using UniversitySchedule.Models.Request;
using UniversitySchedule.Models.Results;

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

		[HttpGet]
		[Authorize]
		[TypeFilter(typeof(PageLimitAttribute))]
		[Route("CheckAuthenticated")]
		public IActionResult CheckAuthenticated(string path)
		{
			return new OkResult();
		}


		[HttpPost]
		public async Task<IActionResult> Post([FromBody] AccountRequest account)
		{
			var user = await UserRepository.GetUserAsync(account.Login, account.Password);

			if (user == null)
			{
				return new BadRequestObjectResult("Неверный логин или пароль.");
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
				expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LifeTimeInMinutes)),
				signingCredentials: new SigningCredentials(AuthOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			var result = new AccountResult
			{
				Token = encodedJwt,
				FirstName = user.FirstName,
				SecondName = user.SecondName
			};

			return new OkObjectResult(result);
		}
	}
}
