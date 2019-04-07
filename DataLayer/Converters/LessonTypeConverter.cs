using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using DbLessonType = Repository.Entities.LessonType;

namespace DataLayer.Converters
{
	public static class LessonTypeConverter
	{
		public static LessonType Convert(DbLessonType dbLessonType)
		{
			if (dbLessonType == null) return null;

			return new LessonType()
			{
				Id = dbLessonType.Id,
				Name = dbLessonType.Name
			};
		}

		public static DbLessonType Convert(LessonType lessonType)
		{
			if (lessonType == null) return null;

			return new DbLessonType()
			{
				Id = lessonType.Id,
				Name = lessonType.Name
			};
		}
	}
}
