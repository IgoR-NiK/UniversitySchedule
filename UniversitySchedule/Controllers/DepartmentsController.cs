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
    public class DepartmentsController : ControllerBase
    {
		IDepartmentRepository DepartmentRepository { get; }

		public DepartmentsController(IDepartmentRepository departmentRepository)
		{
			DepartmentRepository = departmentRepository;
		}


		[HttpGet]
		[Route("GetAdminDepartments")]
		public async Task<IEnumerable<DepartmentResponse>> GetAdminDepartments()
		{
			var departments = await DepartmentRepository.GetEntityListAsync();
			return departments.Select(x => DepartmentConverter.ConvertTo(x));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Department department)
		{
			if (department == null)
				return BadRequest();

			await DepartmentRepository.AddAsync(DepartmentConverter.Convert(department));
			return new OkObjectResult(department);
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Department department)
		{
			if (department == null)
				return BadRequest();

			await DepartmentRepository.UpdateAsync(DepartmentConverter.Convert(department));
			return new OkObjectResult(department);
		}

		[HttpDelete]
		public async Task Delete(int id)
		{
			await DepartmentRepository.DeleteAsync(id);
		}
	}
}