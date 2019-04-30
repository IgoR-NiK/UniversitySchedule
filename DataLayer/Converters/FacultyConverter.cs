using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataLayer.Models;
using DataLayer.Models.Response;
using DbFaculty = Repository.Entities.Faculty;

namespace DataLayer.Converters
{
	public static class FacultyConverter
	{
		public static Faculty Convert(DbFaculty dbFaculty)
		{
			if (dbFaculty == null) return null;

			return new Faculty()
			{
				Id = dbFaculty.Id,
				Name = dbFaculty.Name,
				ShortName = dbFaculty.ShortName
			};
		}

		public static DbFaculty Convert(Faculty faculty)
		{
			if (faculty == null) return null;

			return new DbFaculty()
			{
				Id = faculty.Id,
				Name = faculty.Name,
				ShortName = faculty.ShortName
			};
		}

		public static FacultyResponse ConvertTo(DbFaculty dbFaculty)
		{
			if (dbFaculty == null) return null;

			return new FacultyResponse()
			{
				Name = dbFaculty.Name,
				ShortName = dbFaculty.ShortName
			};
		}
	}
}