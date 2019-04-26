using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Models.Response;
using Repository.Entities;

namespace DataLayer.Converters
{
	public static class ScheduleConverter
	{
		public static List<WeekResponse> Convert(List<PeriodTimeslot> periodTimeslots)
		{
			var weeks = new List<WeekResponse>();

			var dbWeeks = periodTimeslots.GroupBy(x => x.Week, new WeekComparer());
			foreach(var dbWeek in dbWeeks)
			{
				var days = new List<DayResponse>();

				var dbDays = dbWeek.GroupBy(x => x.Day, new DayComparer());
				foreach (var dbDay in dbDays)
				{
					var dayTimeslots = new List<DayTimeslotResponse>();

					foreach (var periodTimeslot in dbDay.OrderBy(x => x.DayTimeslotId))
					{
						var scheduleCell = periodTimeslot.ScheduleCells.Count > 0 ? periodTimeslot.ScheduleCells.ToList()[0] : null;

						var teacher = scheduleCell?.TeachingUnit.Teacher;
						var teacherName = teacher == null
							? null
							: $"{teacher.SecondName} {teacher.FirstName.Substring(0, 1)}. {teacher.MiddleName?.Substring(0, 1)}.";

						var lessonType = scheduleCell?.TeachingUnit.LessonType;

						dayTimeslots.Add(new DayTimeslotResponse()
						{
							Number = periodTimeslot.DayTimeslot.Id,
							StartTime = periodTimeslot.DayTimeslot.StartTime.ToShortTimeString(),
							EndTime = periodTimeslot.DayTimeslot.StartTime.AddMinutes(90).ToShortTimeString(),
							CourseName = scheduleCell?.TeachingUnit.Course.Name,
							LessonTypeName = lessonType == null ? null : $"({lessonType.Name})",
							Classroom = scheduleCell?.Classroom.Name,
							GroupName = scheduleCell?.TeachingUnit.Group.Name,
							TeacherName = teacherName,
							TeacherPost = teacher == null ? null : $"({teacher.Post.Description})"
						});
					}

					var difference = (int)DateTime.Now.DayOfWeek - dbDay.Key.DayOfWeek;					
					var calcDate = DateTime.Now.AddDays(-difference);

					days.Add(new DayResponse() { Name = dbDay.Key.Name, Date = $"{calcDate:M}", DayTimeslotsResponse = dayTimeslots });
				}

				weeks.Add(new WeekResponse() { Name = dbWeek.Key.Name, DaysResponse = days });
			}

			return weeks;
		}
	}

	class WeekComparer : IEqualityComparer<Week>
	{
		public bool Equals(Week x, Week y)
		{
			return x.Id == y.Id;
		}

		public int GetHashCode(Week obj)
		{
			return obj.Id.GetHashCode();
		}
	}

	class DayComparer : IEqualityComparer<Day>
	{
		public bool Equals(Day x, Day y)
		{
			return x.Id == y.Id;
		}

		public int GetHashCode(Day obj)
		{
			return obj.Id.GetHashCode();
		}
	}
}
