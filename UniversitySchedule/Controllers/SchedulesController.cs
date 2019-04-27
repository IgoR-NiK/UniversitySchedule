using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using DataLayer.Converters;
using DataLayer.Models.Response;
using DataLayer.Models.Enums;
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

			var schedule = await ScheduleRepository.GetEntityListAsync();

			var cells = new List<List<ScheduleCellResponse>>();

			foreach(var building in buildings)
			{
				foreach(var room in building.ClassroomsResponse)
				{
					var row = new List<ScheduleCellResponse>();

					foreach (var week in weeks)
					{
						foreach (var day in week.DaysResponse)
						{
							foreach(var dayTimeslot in day.DayTimeslotsResponse)
							{
								var cell = new ScheduleCellResponse();

								var isBlocked = classrooms
									.FirstOrDefault(c => c.Id == room.Id)
									?.BanClassroomPeriodTimeslots
									.Any(b => b.PeriodTimeslotId == dayTimeslot.Id) ?? false;

								if (isBlocked)
									cell.CellType = CellType.Blocked;


								var teachingUnit = schedule
									.FirstOrDefault(s => s.ClassroomId == room.Id && s.PeriodTimeslotId == dayTimeslot.Id)
									?.TeachingUnit;

								if (teachingUnit != null)
								{
									cell.GroupName = $"{teachingUnit.Group.Name} ({teachingUnit.Group.StudentsCount} чел.)";
									cell.CourseName = teachingUnit.Course.Name;
									cell.LessonTypeName = $"{teachingUnit.LessonType.Name}";
									cell.TeacherName = $"{teachingUnit.Teacher.SecondName} {teachingUnit.Teacher.FirstName.Substring(0, 1)}. {teachingUnit.Teacher.MiddleName?.Substring(0, 1)}.";
									cell.TeacherPost = $"({teachingUnit.Teacher.Post.Description})";

									cell.CellType =
										teachingUnit.LessonTypeId == 1 ? CellType.Lecture :
										teachingUnit.LessonTypeId == 2 ? CellType.Practice :
										teachingUnit.LessonTypeId == 3 ? CellType.LaboratoryWork : 
										CellType.Empty;
								}

								row.Add(cell);
							}
						}
					}

					cells.Add(row);
				}
			}

			return new OkObjectResult(new GridResponse()
			{
				Weeks = weeks,
				Buildings = buildings,
				Cells = cells
			});
		}
	}
}
