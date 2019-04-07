using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using DbClassroomType = Repository.Entities.ClassroomType;

namespace DataLayer.Converters
{
	public static class ClassroomTypeConverter
	{
		public static ClassroomType Convert(DbClassroomType dbClassroomType)
		{
			if (dbClassroomType == null) return null;

			return new ClassroomType()
			{
				Id = dbClassroomType.Id,
				Name = dbClassroomType.Name
			};
		}

		public static DbClassroomType Convert(ClassroomType classroomType)
		{
			if (classroomType == null) return null;

			return new DbClassroomType()
			{
				Id = classroomType.Id,
				Name = classroomType.Name
			};
		}
	}
}
