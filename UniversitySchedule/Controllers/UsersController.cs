using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using DataLayer.Models;
using DataLayer.Converters;
using Repository.Interfaces;
using DataLayer.Models.Response;

namespace UniversitySchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
		IUserRepository UserRepository { get; }

		public UsersController(IUserRepository userRepository)
		{
			UserRepository = userRepository;
		}


		[HttpGet]
		[Route("GetAdminUsers")]
		public async Task<IEnumerable<UserResponse>> GetAdminUsers()
		{
			var users = await UserRepository.GetEntityListAsync();
			return users.Select(x => UserConverter.ConvertTo(x));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] User user)
		{
			if (user == null)
				return BadRequest();

			await UserRepository.AddAsync(UserConverter.Convert(user));
			return new OkObjectResult(user);
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] User user)
		{
			if (user == null)
				return BadRequest();

			await UserRepository.UpdateAsync(UserConverter.Convert(user));
			return new OkObjectResult(user);
		}

		[HttpDelete]
		public async Task Delete(int id)
		{
			await UserRepository.DeleteAsync(id);
		}
	}
}