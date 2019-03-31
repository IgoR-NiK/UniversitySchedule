﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniversitySchedule.Models;
using DbGroup = Repository.Entities.Group;

namespace UniversitySchedule.Converters
{
	public static class GroupConverter
	{
		public static Group Convert(DbGroup dbGroup)
		{
			return new Group()
			{
				Id = dbGroup.Id,
				Name = dbGroup.Name,
				CoursesNumber = dbGroup.CoursesNumber,
				StudentsCount = dbGroup.StudentsCount
			};
		}

		public static DbGroup Convert(Group group)
		{
			return new DbGroup()
			{
				Id = group.Id,
				Name = group.Name,
				CoursesNumber = group.CoursesNumber,
				StudentsCount = group.StudentsCount
			};
		}
	}
}