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
			throw new NotImplementedException();
		}

		public Task AddRangeAsync(IEnumerable<Classroom> items)
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

		public Task<Classroom> GetEntityAsync(int id)
		{
			throw new NotImplementedException();
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
			throw new NotImplementedException();
		}
	}
}
