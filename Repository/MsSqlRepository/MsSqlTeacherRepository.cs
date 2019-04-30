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
	public class MsSqlTeacherRepository : MsSqlBaseRepository, ITeacherRepository
	{
		public MsSqlTeacherRepository(ConnectionOptions options) 
			: base(options) { }

		public Task<Teacher> AddAsync(Teacher item)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Teachers.Add(item);
					dbContext.SaveChanges();
				}

				return item;
			});
		}

		public Task AddRangeAsync(IEnumerable<Teacher> items)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Teachers.AddRange(items);
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
					dbContext.Teachers.RemoveRange(dbContext.Teachers);
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
					var item = dbContext.Teachers.Find(id);
					if (item != null)
					{
						dbContext.Teachers.Remove(item);
						dbContext.SaveChanges();
					}
				}
			});
		}

		public Task<Teacher> GetEntityAsync(int id)
		{
			return Task.Run(() =>
			{
				Teacher teacher;

				using (var dbContext = GetDbContext())
				{
					teacher = dbContext.Teachers.FirstOrDefault(x => x.Id == id);
				}

				return teacher;
			});
		}

		public Task<List<Teacher>> GetEntityListAsync()
		{
			return Task.Run(() =>
			{
				var teachers = new List<Teacher>();

				using (var dbContext = GetDbContext())
				{
					teachers = dbContext.Teachers
									.Include(x => x.Department)
									.Include(x => x.Post)
									.OrderBy(x => x.SecondName)
									.ToList();
				}

				return teachers;
			});
		}

		public Task<Teacher> UpdateAsync(Teacher item)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Teachers.Update(item);
					dbContext.SaveChanges();
				}

				return item;
			});
		}
	}
}
