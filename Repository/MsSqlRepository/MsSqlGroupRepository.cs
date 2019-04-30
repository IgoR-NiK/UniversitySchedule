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
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Groups.Add(item);
					dbContext.SaveChanges();
				}

				return item;
			});
		}

		public Task AddRangeAsync(IEnumerable<Group> items)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Groups.AddRange(items);
					dbContext.SaveChanges();
				}
			});
		}

		public Task Clear()
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Groups.RemoveRange(dbContext.Groups);
					dbContext.SaveChanges();
				}
			});
		}

		public Task DeleteAsync(int id)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					var item = dbContext.Groups.Find(id);
					if (item != null)
					{
						dbContext.Groups.Remove(item);
						dbContext.SaveChanges();
					}
				}
			});
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
			return Task.Run(() =>
			{
				var groups = new List<Group>();

				using (var dbContext = GetDbContext())
				{
					groups = dbContext.Groups
								.Include(x => x.Department)			
								.OrderBy(f => f.Name)
								.ToList();
				}

				return groups;
			});
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
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Groups.Update(item);
					dbContext.SaveChanges();
				}

				return item;
			});
		}
	}
}
