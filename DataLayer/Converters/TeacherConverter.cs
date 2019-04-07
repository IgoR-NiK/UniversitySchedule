using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Models;
using DbTeacher = Repository.Entities.Teacher;

namespace DataLayer.Converters
{
	public static class TeacherConverter
	{
		public static Teacher Convert(DbTeacher dbTeacher)
		{
			if (dbTeacher == null) return null;

			return new Teacher()
			{
				Id = dbTeacher.Id,
				FirstName = dbTeacher.FirstName,
				SecondName = dbTeacher.SecondName,
				MiddleName = dbTeacher.MiddleName,
				Gender = dbTeacher.Gender,
				Birthday = dbTeacher.Birthday,

				DepartmentId = dbTeacher.DepartmentId,
				Department = DepartmentConverter.Convert(dbTeacher.Department),

				PostId = dbTeacher.PostId,
				Post = PostConverter.Convert(dbTeacher.Post),

				BanPeriodTimeslots = dbTeacher.BanTeacherPeriodTimeslots
										.Select(x => PeriodTimeslotConverter.Convert(x.PeriodTimeslot))
										.ToList()
			};
		}

		public static DbTeacher Convert(Teacher teacher)
		{
			if (teacher == null) return null;

			return new DbTeacher()
			{
				Id = teacher.Id,
				FirstName = teacher.FirstName,
				SecondName = teacher.SecondName,
				MiddleName = teacher.MiddleName,
				Gender = teacher.Gender,
				Birthday = teacher.Birthday,

				DepartmentId = teacher.DepartmentId,
				PostId = teacher.PostId
			};
		}
	}
}
