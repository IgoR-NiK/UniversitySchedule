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
    public class TeachersController : ControllerBase
    {
		ITeacherRepository TeacherRepository { get; }

		public TeachersController(ITeacherRepository teacherRepository)
		{
			TeacherRepository = teacherRepository;
		}


		[HttpGet]
		[Route("GetAdminTeachers")]
		public async Task<IEnumerable<TeacherResponse>> GetAdminTeachers()
		{
			var teachers = await TeacherRepository.GetEntityListAsync();
			return teachers.Select(x => TeacherConverter.ConvertTo(x));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Teacher teacher)
		{
			if (teacher == null)
				return BadRequest();

			await TeacherRepository.AddAsync(TeacherConverter.Convert(teacher));
			return new OkObjectResult(teacher);
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Teacher teacher)
		{
			if (teacher == null)
				return BadRequest();

			await TeacherRepository.UpdateAsync(TeacherConverter.Convert(teacher));
			return new OkObjectResult(teacher);
		}

		[HttpDelete]
		public async Task Delete(int id)
		{
			await TeacherRepository.DeleteAsync(id);
		}
	}
}