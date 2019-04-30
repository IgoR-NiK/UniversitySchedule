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
	public class MsSqlClassroomRepository : MsSqlBaseRepository, IClassroomRepository
	{
		public MsSqlClassroomRepository(ConnectionOptions options) 
			: base(options) { }

		public Task<Classroom> AddAsync(Classroom item)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Classrooms.Add(item);
					dbContext.SaveChanges();
				}

				return item;
			});
		}

		public Task AddRangeAsync(IEnumerable<Classroom> items)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Classrooms.AddRange(items);
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
					dbContext.Classrooms.RemoveRange(dbContext.Classrooms);
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
					var item = dbContext.Classrooms.Find(id);
					if (item != null)
					{
						dbContext.Classrooms.Remove(item);
						dbContext.SaveChanges();
					}
				}
			});
		}

		public Task<Classroom> GetEntityAsync(int id)
		{
			return Task.Run(() =>
			{
				Classroom classroom;

				using (var dbContext = GetDbContext())
				{
					classroom = dbContext.Classrooms.FirstOrDefault(x => x.Id == id);
				}

				return classroom;
			});
		}

		public Task<List<Classroom>> GetEntityListAsync()
		{
			return Task.Run(async () =>
			{
				var classrooms = new List<Classroom>();

				using (var dbContext = GetDbContext())
				{
					classrooms = await dbContext.Classrooms
								.Include(x => x.Building)
								.Include(x => x.ClassroomType)
								.Include(x => x.BanClassroomPeriodTimeslots)
									.ThenInclude(x => x.PeriodTimeslot)
								.OrderBy(x => x.Id)
								.ToListAsync();
				}

				return classrooms;
			});
		}

		public Task<Classroom> UpdateAsync(Classroom item)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Classrooms.Update(item);
					dbContext.SaveChanges();
				}

				return item;
			});
		}
	}
}
