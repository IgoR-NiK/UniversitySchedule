using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Repository.Entities;
using Repository.Interfaces;

namespace Repository.MsSqlRepository
{
	public class MsSqlGroupRepository : MsSqlBaseRepository, IGroupRepository
	{
		public MsSqlGroupRepository(ConnectionOptions options)
			: base(options) { }


		public Task<Group> AddAsync(Group item)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Group> GetEntityAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<Group>> GetEntityListAsync()
		{
			throw new NotImplementedException();
		}

		public Task<List<Group>> GetGroupsForFacultyAndCourseAsync(int facultyId, int courseNumber)
		{
			return Task.Run(() =>
			{
				var groups = new List<Group>();

				using (var dbContext = GetDbContext())
				{
					groups = dbContext.Groups
								.Include(g => g.Department)
								.Where(g => g.Department.FacultyId == facultyId && g.CoursesNumber == courseNumber)
								.ToList();
				}

				return groups;				
			});
		}

		public Task<Group> UpdateAsync(Group item)
		{
			throw new NotImplementedException();
		}
	}
}
