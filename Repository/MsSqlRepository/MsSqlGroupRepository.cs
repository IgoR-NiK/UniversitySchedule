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

		public Task AddRangeAsync(IEnumerable<Group> items)
		{
			throw new NotImplementedException();
		}

		public Task Clear()
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Group> GetEntityAsync(int id)
		{
			return Task.Run(() =>
			{
				Group group;

				using (var dbContext = GetDbContext())
				{
					group = dbContext.Groups.FirstOrDefault(x => x.Id == id);
				}

				return group;
			});
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
								.Include(x => x.Department)
								.Where(x => x.Department.FacultyId == facultyId && x.CoursesNumber == courseNumber)
								.OrderBy(x => x.Name)
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
