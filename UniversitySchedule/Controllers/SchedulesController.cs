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
		IPeriodTimeslotRepository PeriodTimeslotRepository { get; }
		IClassroomRepository ClassroomRepository { get; }

		public SchedulesController(
			IScheduleRepository scheduleRepository,
			IPeriodTimeslotRepository periodTimeslotRepository,
			IClassroomRepository classroomRepository)
		{
			ScheduleRepository = scheduleRepository;
			PeriodTimeslotRepository = periodTimeslotRepository;
			ClassroomRepository = classroomRepository;
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

		[HttpGet]
		[Route("GetScheduleGrid")]
		public async Task<IActionResult> GetScheduleGrid()
		{
			var periodTimeslots = await PeriodTimeslotRepository.GetEntityListAsync();
			var weeks = ScheduleConverter.Convert(periodTimeslots);

			var classrooms = await ClassroomRepository.GetEntityListAsync();
			var buildings  = ScheduleConverter.Convert(classrooms);







			return new OkObjectResult(buildings);
		}
	}
}
