using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
using Repository.Interfaces;

namespace Repository.MsSqlRepository
{
	public class MsSqlScheduleRepository : MsSqlBaseRepository, IScheduleRepository
	{
		public MsSqlScheduleRepository(ConnectionOptions options) 
			: base(options) { }

		public Task<Schedule> AddAsync(Schedule item)
		{
			throw new NotImplementedException();
		}

		public Task AddRangeAsync(IEnumerable<Schedule> items)
		{
			return Task.Run(async() =>
			{
				using (var dbContext = GetDbContext())
				{
					await dbContext.AddRangeAsync(items);
					await dbContext.SaveChangesAsync();
				}
			});
		}

		public Task Clear()
		{
			return Task.Run(async () =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Schedules.RemoveRange(dbContext.Schedules);
					await dbContext.SaveChangesAsync();
				}
			});
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Schedule> GetEntityAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<Schedule>> GetEntityListAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Schedule> UpdateAsync(Schedule item)
		{
			throw new NotImplementedException();
		}
	}
}
