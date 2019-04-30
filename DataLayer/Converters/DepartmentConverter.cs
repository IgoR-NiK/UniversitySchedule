using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using DataLayer.Models.Response;
using DbDepartment = Repository.Entities.Department;

namespace DataLayer.Converters
{
	public static class DepartmentConverter
	{
		public static Department Convert(DbDepartment dbDepartment)
		{
			if (dbDepartment == null) return null;

			return new Department()
			{
				Id = dbDepartment.Id,
				Name = dbDepartment.Name,

				FacultyId = dbDepartment.FacultyId,
				Faculty = FacultyConverter.Convert(dbDepartment.Faculty)
			};
		}

		public static DbDepartment Convert(Department department)
		{
			if (department == null) return null;

			return new DbDepartment()
			{
				Id = department.Id,
				Name = department.Name,
				FacultyId = department.FacultyId
			};
		}

		public static DepartmentResponse ConvertTo(DbDepartment dbDepartment)
		{
			if (dbDepartment == null) return null;

			return new DepartmentResponse()
			{	
				Name = dbDepartment.Name,
				FacultyName = dbDepartment.Faculty.Name
			};
		}
	}
}