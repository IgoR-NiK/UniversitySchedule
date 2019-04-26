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
	public class MsSqlPeriodTimeslotRepository : MsSqlBaseRepository, IPeriodTimeslotRepository
	{
		public MsSqlPeriodTimeslotRepository(ConnectionOptions options) 
			: base(options) { }

		public Task<PeriodTimeslot> AddAsync(PeriodTimeslot item)
		{
			throw new NotImplementedException();
		}

		public Task AddRangeAsync(IEnumerable<PeriodTimeslot> items)
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

		public Task<PeriodTimeslot> GetEntityAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<PeriodTimeslot>> GetEntityListAsync()
		{
			return Task.Run(() =>
			{
				var periodTimeslots = new List<PeriodTimeslot>();

				using (var dbContext = GetDbContext())
				{
					periodTimeslots = dbContext.PeriodTimeslots
								.Include(x => x.Week)
								.Include(x => x.Day)
								.Include(x => x.DayTimeslot)
								.OrderBy(x => x.Id)
								.ToList();
				}

				return periodTimeslots;
			});
		}

		public Task<PeriodTimeslot> UpdateAsync(PeriodTimeslot item)
		{
			throw new NotImplementedException();
		}
	}
}
