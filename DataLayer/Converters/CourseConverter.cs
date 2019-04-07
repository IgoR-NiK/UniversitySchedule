using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using DbCourse = Repository.Entities.Course;

namespace DataLayer.Converters
{
	public static class CourseConverter
	{
		public static Course Convert(DbCourse dbCourse)
		{
			if (dbCourse == null) return null;

			return new Course()
			{
				Id = dbCourse.Id,
				Name = dbCourse.Name
			};
		}

		public static DbCourse Convert(Course course)
		{
			if (course == null) return null;

			return new DbCourse()
			{
				Id = course.Id,
				Name = course.Name
			};
		}
	}
}
