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
    public class ClassroomsController : ControllerBase
    {
		IClassroomRepository ClassroomRepository { get; }

		public ClassroomsController(IClassroomRepository classroomRepository)
		{
			ClassroomRepository = classroomRepository;
		}


		[HttpGet]
		[Route("GetAdminClassrooms")]
		public async Task<IEnumerable<ClassroomResponse>> GetAdminClassrooms()
		{
			var classrooms = await ClassroomRepository.GetEntityListAsync();
			return classrooms.Select(x => ClassroomConverter.ConvertTo(x));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Classroom classroom)
		{
			if (classroom == null)
				return BadRequest();

			await ClassroomRepository.AddAsync(ClassroomConverter.Convert(classroom));
			return new OkObjectResult(classroom);
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Classroom classroom)
		{
			if (classroom == null)
				return BadRequest();

			await ClassroomRepository.UpdateAsync(ClassroomConverter.Convert(classroom));
			return new OkObjectResult(classroom);
		}

		[HttpDelete]
		public async Task Delete(int id)
		{
			await ClassroomRepository.DeleteAsync(id);
		}
	}
}