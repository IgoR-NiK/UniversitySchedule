using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Models;
using DbTeachingUnit = Repository.Entities.TeachingUnit;

namespace DataLayer.Converters
{
	public static class TeachingUnitConverter
	{
		public static TeachingUnit Convert(DbTeachingUnit dbTeachingUnit)
		{
			if (dbTeachingUnit == null) return null;
			
			return new TeachingUnit()
			{
				Id = dbTeachingUnit.Id,
				CountInPeriodTimeslot = dbTeachingUnit.CountInPeriodTimeslot,

				GroupId = dbTeachingUnit.GroupId,
				Group = GroupConverter.Convert(dbTeachingUnit.Group),

				TeacherId = dbTeachingUnit.TeacherId,
				Teacher = TeacherConverter.Convert(dbTeachingUnit.Teacher),

				CourseId = dbTeachingUnit.CourseId,
				Course = CourseConverter.Convert(dbTeachingUnit.Course),

				LessonTypeId = dbTeachingUnit.LessonTypeId,
				LessonType = LessonTypeConverter.Convert(dbTeachingUnit.LessonType),

				ClassroomTypes = dbTeachingUnit.TeachingUnitClassroomTypes
										.Select(x => ClassroomTypeConverter.Convert(x.ClassroomType))
										.ToList()
			};
		}

		public static DbTeachingUnit Convert(TeachingUnit teachingUnit)
		{
			if (teachingUnit == null) return null;

			return new DbTeachingUnit()
			{
				Id = teachingUnit.Id,
				CountInPeriodTimeslot = teachingUnit.CountInPeriodTimeslot,
				GroupId = teachingUnit.GroupId,
				TeacherId = teachingUnit.TeacherId,
				CourseId = teachingUnit.CourseId,
				LessonTypeId = teachingUnit.LessonTypeId
			};
		}
	}
}
