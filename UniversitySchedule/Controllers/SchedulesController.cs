using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using DataLayer.Converters;
using Repository.Interfaces;

namespace UniversitySchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
		IScheduleRepository ScheduleRepository { get; }

		public SchedulesController(IScheduleRepository scheduleRepository)
		{
			ScheduleRepository = scheduleRepository;
		}


		[HttpGet]
		[Route("GetScheduleForGroup")]
		public async Task<IActionResult> GetScheduleForGroup(int groupId)
		{
			var periodTimeslots = await ScheduleRepository.GetScheduleForGroupAsync(groupId);
			var weeks = ScheduleConverter.Convert(periodTimeslots);
			return new OkObjectResult(weeks);
		}

		[HttpGet]
		[Route("GetScheduleForTeacher")]
		public async Task<IActionResult> GetScheduleForTeacher(int teacherId)
		{
			var periodTimeslots = await ScheduleRepository.GetScheduleForTeacherAsync(teacherId);
			var weeks = ScheduleConverter.Convert(periodTimeslots);
			return new OkObjectResult(weeks);
		}
	}
}
