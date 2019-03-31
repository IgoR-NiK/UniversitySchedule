using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Repository.Entities;

namespace Repository.Interfaces
{
	public interface IGroupRepository : IRepository<Group>
	{
		Task<List<Group>> GetGroupsForFacultyAndCourseAsync(int facultyId, int courseNumber);
	}
}
