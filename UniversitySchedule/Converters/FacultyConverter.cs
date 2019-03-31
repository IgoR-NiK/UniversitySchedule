using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniversitySchedule.Models;
using DbFaculty = Repository.Entities.Faculty;

namespace UniversitySchedule.Converters
{
	public static class FacultyConverter
	{
		public static Faculty Convert(DbFaculty dbFaculty)
		{
			return new Faculty()
			{
				Id = dbFaculty.Id,
				Name = dbFaculty.Name,
				ShortName = dbFaculty.ShortName
			};
		}

		public static DbFaculty Convert(Faculty faculty)
		{
			return new DbFaculty()
			{
				Id = faculty.Id,
				Name = faculty.Name,
				ShortName = faculty.ShortName
			};
		}
	}
}