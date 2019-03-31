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
	public class MsSqlFacultyRepository : MsSqlBaseRepository, IFacultyRepository
	{
		public MsSqlFacultyRepository(ConnectionOptions options) 
			: base(options) { }

		public Task<Faculty> AddAsync(Faculty item)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<int>> GetCoursesForFacultyAsync(int facultyId)
		{
			return Task.Run(() =>
			{
				var courses = new List<int>();

				using (var dbContext = GetDbContext())
				{
					courses = dbContext.Faculties
								.Include(f => f.Departments)
									.ThenInclude(d => d.Groups)
								.FirstOrDefault(f => f.Id == facultyId)
								?.Departments.SelectMany(d => d.Groups, (department, group) => group.CoursesNumber)
								.Distinct()
								.OrderBy(x => x)
								.ToList() ?? courses;
				}

				return courses;
			});
		}

		public Task<Faculty> GetEntityAsync(int id)
		{
			return Task.Run(() =>
			{
				Faculty faculty;

				using (var dbContext = GetDbContext())
				{
					faculty = dbContext.Faculties.FirstOrDefault(x => x.Id == id);
				}

				return faculty;
			});
		}

		public Task<List<Faculty>> GetEntityListAsync()
		{
			return Task.Run(() =>
			{
				var faculties = new List<Faculty>();

				using(var dbContext = GetDbContext())
				{
					faculties = dbContext.Faculties.OrderBy(f => f.Name).ToList();
				}

				return faculties;
			});
		}

		public Task<Faculty> UpdateAsync(Faculty item)
		{
			throw new NotImplementedException();
		}
	}
}
