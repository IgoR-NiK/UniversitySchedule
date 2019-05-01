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
    public class RolesController : ControllerBase
    {
		IRoleRepository RoleRepository { get; }

		public RolesController(IRoleRepository roleRepository)
		{
			RoleRepository = roleRepository;
		}

		[HttpGet]
		[Route("GetAdminRoles")]
		public async Task<IEnumerable<RoleResponse>> GetAdminRoles()
		{
			var roles = await RoleRepository.GetEntityListAsync();
			return roles.Select(x => RoleConverter.ConvertTo(x));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Role role)
		{
			if (role == null)
				return BadRequest();

			await RoleRepository.AddAsync(RoleConverter.Convert(role));
			return new OkObjectResult(role);
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Role role)
		{
			if (role == null)
				return BadRequest();

			await RoleRepository.UpdateAsync(RoleConverter.Convert(role));
			return new OkObjectResult(role);
		}

		[HttpDelete]
		public async Task Delete(int id)
		{
			await RoleRepository.DeleteAsync(id);
		}
	}
}