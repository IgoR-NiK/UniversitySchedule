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
using DataLayer.ScheduleGenerations.Genetic;
using UniversitySchedule.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace UniversitySchedule.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SchedulesController : ControllerBase
	{
		IScheduleRepository ScheduleRepository { get; }
		IPeriodTimeslotRepository PeriodTimeslotRepository { get; }
		IClassroomRepository ClassroomRepository { get; }
		ITeachingUnitRepository TeachingUnitRepository { get; }
		IScheduleCellRepository ScheduleCellRepository { get; }
		IHubContext<ScheduleHub> HubContext { get; }

		public SchedulesController(
			IScheduleRepository scheduleRepository,
			IPeriodTimeslotRepository periodTimeslotRepository,
			IClassroomRepository classroomRepository,
			ITeachingUnitRepository teachingUnitRepository,
			IScheduleCellRepository scheduleCellRepository,
			IHubContext<ScheduleHub> hubContext)
		{
			ScheduleRepository = scheduleRepository;
			PeriodTimeslotRepository = periodTimeslotRepository;
			ClassroomRepository = classroomRepository;
			TeachingUnitRepository = teachingUnitRepository;
			ScheduleCellRepository = scheduleCellRepository;
			HubContext = hubContext;
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
			var grid = await GetScheduleGridPrivate();
			return new OkObjectResult(grid);
		}

		private async Task<GridResponse> GetScheduleGridPrivate()
		{
			var periodTimeslots = await PeriodTimeslotRepository.GetEntityListAsync();
			var weeks = ScheduleConverter.Convert(periodTimeslots);

			var classrooms = await ClassroomRepository.GetEntityListAsync();
			var buildings = ScheduleConverter.Convert(classrooms);

			var schedule = await ScheduleRepository.GetEntityListAsync();

			var cells = new List<List<ScheduleCellResponse>>();

			foreach (var building in buildings)
			{
				foreach (var room in building.ClassroomsResponse)
				{
					var row = new List<ScheduleCellResponse>();

					foreach (var week in weeks)
					{
						foreach (var day in week.DaysResponse)
						{
							foreach (var dayTimeslot in day.DayTimeslotsResponse)
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

			return new GridResponse()
			{
				Weeks = weeks,
				Buildings = buildings,
				Cells = cells
			};
		}

		[HttpGet]
		[Route("GenerateSchedule")]
		public async Task GenerateSchedule()
		{
			var periodTimeslots = (await PeriodTimeslotRepository.GetEntityListAsync())
				.Select(x => PeriodTimeslotConverter.Convert(x)).ToList();

			var classrooms = (await ClassroomRepository.GetEntityListAsync())
				.Select(x => ClassroomConverter.Convert(x)).ToList();

			var teachingUnits = (await TeachingUnitRepository.GetEntityListAsync())
				.Select(x => TeachingUnitConverter.Convert(x)).ToList();

			var generation = new GeneticScheduleGeneration(10, teachingUnits.Count);

			generation.GlobalGA.IterationCompleted += async () =>
			{
				await HubContext.Clients.All.SendAsync("SendScheduleInfo", new ScheduleInfo()
				{
					MaxValue = generation.GlobalGA.Pool.Max(x => x.Value),
					AverageValue = generation.GlobalGA.Pool.Average(x => x.Value),
					AverageAge = generation.GlobalGA.Pool.Average(x => x.Age)
				});
			};

			var schedules = generation.Run(classrooms, periodTimeslots, teachingUnits);
			
			await ScheduleCellRepository.Clear();
			await ScheduleCellRepository.AddRangeAsync(schedules.First().ScheduleCells.Select(x => ScheduleCellConverter.Convert(x)));

			var gridResponse = await GetScheduleGridPrivate();
			await HubContext.Clients.All.SendAsync("GenerateScheduleCompleted", gridResponse);
		}
	}
}
