using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Repository.Entities;

namespace Repository.Interfaces
{
	public interface IFacultyRepository : IRepository<Faculty>
	{
		Task<List<int>> GetCoursesForFacultyAsync(int facultyId);
	}
}
