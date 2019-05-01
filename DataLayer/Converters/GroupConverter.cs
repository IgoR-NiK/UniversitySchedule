using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataLayer.Models;
using DataLayer.Models.Response;
using DbGroup = Repository.Entities.Group;

namespace DataLayer.Converters
{
	public static class GroupConverter
	{
		public static Group Convert(DbGroup dbGroup)
		{
			if (dbGroup == null) return null;

			return new Group()
			{
				Id = dbGroup.Id,
				Name = dbGroup.Name,
				CoursesNumber = dbGroup.CoursesNumber,
				StudentsCount = dbGroup.StudentsCount,

				DepartmentId = dbGroup.DepartmentId,
				Department = DepartmentConverter.Convert(dbGroup.Department),

				ParentGroupId = dbGroup.ParentGroupId,

				ChildGroups = dbGroup.ChildGroups
								.Select(x => GroupConverter.Convert(x))
								.ToList()
			};
		}

		public static DbGroup Convert(Group group)
		{
			if (group == null) return null;

			return new DbGroup()
			{
				Id = group.Id,
				Name = group.Name,
				CoursesNumber = group.CoursesNumber,
				StudentsCount = group.StudentsCount,
				DepartmentId = group.DepartmentId,
				ParentGroupId = group.ParentGroupId
			};
		}

		public static GroupResponse ConvertTo(DbGroup dbGroup)
		{
			if (dbGroup == null) return null;

			return new GroupResponse()
			{
				Name = dbGroup.Name,
				StudentCount = dbGroup.StudentsCount,
				NumberCourse = dbGroup.CoursesNumber,
				DepartmentName = dbGroup.Department?.Name
			};
		}
	}
}