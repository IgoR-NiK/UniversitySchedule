using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
using Repository.Interfaces;

namespace Repository.MsSqlRepository
{
	public class MsSqlScheduleCellRepository : MsSqlBaseRepository, IScheduleCellRepository
	{
		public MsSqlScheduleCellRepository(ConnectionOptions options) 
			: base(options) { }

		public Task<ScheduleCell> AddAsync(ScheduleCell item)
		{
			throw new NotImplementedException();
		}

		public Task AddRangeAsync(IEnumerable<ScheduleCell> items)
		{
			return Task.Run(async() =>
			{
				using (var dbContext = GetDbContext())
				{
					await dbContext.ScheduleCells.AddRangeAsync(items);
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
					dbContext.ScheduleCells.RemoveRange(dbContext.ScheduleCells);
					await dbContext.SaveChangesAsync();
				}
			});
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<ScheduleCell> GetEntityAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<ScheduleCell>> GetEntityListAsync()
		{
			throw new NotImplementedException();
		}

		public Task<ScheduleCell> UpdateAsync(ScheduleCell item)
		{
			throw new NotImplementedException();
		}
	}
}
