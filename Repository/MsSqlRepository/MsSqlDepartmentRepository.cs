using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.MsSqlRepository
{
	public class MsSqlDepartmentRepository : MsSqlBaseRepository, IDepartmentRepository
	{
		public MsSqlDepartmentRepository(ConnectionOptions options) 
			: base(options) { }


		public Task<Department> AddAsync(Department item)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Departments.Add(item);
					dbContext.SaveChanges();
				}

				return item;
			});
		}

		public Task AddRangeAsync(IEnumerable<Department> items)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Departments.AddRange(items);
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
					dbContext.Departments.RemoveRange(dbContext.Departments);
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
					var item = dbContext.Departments.Find(id);
					if (item != null)
					{
						dbContext.Departments.Remove(item);
						dbContext.SaveChanges();
					}
				}
			});
		}

		public Task<Department> GetEntityAsync(int id)
		{
			return Task.Run(() =>
			{
				Department department;

				using (var dbContext = GetDbContext())
				{
					department = dbContext.Departments.FirstOrDefault(x => x.Id == id);
				}

				return department;
			});
		}

		public Task<List<Department>> GetEntityListAsync()
		{
			return Task.Run(() =>
			{
				var departments = new List<Department>();

				using (var dbContext = GetDbContext())
				{
					departments = dbContext.Departments
									.Include(x => x.Faculty)
									.OrderBy(x => x.Name)
									.ToList();
				}

				return departments;
			});
		}

		public Task<Department> UpdateAsync(Department item)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Departments.Update(item);
					dbContext.SaveChanges();
				}

				return item;
			});
		}
	}
}
