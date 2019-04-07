using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Repository.Entities;
using Repository.Interfaces;

namespace Repository.MsSqlRepository
{
	public class MsSqlTeachingUnitRepository : MsSqlBaseRepository, ITeachingUnitRepository
	{
		public MsSqlTeachingUnitRepository(ConnectionOptions options) 
			: base(options) { }

		public Task<TeachingUnit> AddAsync(TeachingUnit item)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<TeachingUnit> GetEntityAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<TeachingUnit>> GetEntityListAsync()
		{
			return Task.Run(async () =>
			{
				var teachingUnits = new List<TeachingUnit>();

				using (var dbContext = GetDbContext())
				{
					teachingUnits = await dbContext.TeachingUnits
								.Include(x => x.Group)
									.ThenInclude(x => x.Department)
										.ThenInclude(x => x.Faculty)
								.Include(x => x.Group)
									.ThenInclude(x => x.ParentGroup)
								.Include(x => x.Group)
									.ThenInclude(x => x.ChildGroups)

								.Include(x => x.Teacher)
									.ThenInclude(x => x.Department)
										.ThenInclude(x => x.Faculty)
								.Include(x => x.Teacher)
									.ThenInclude(x => x.Post)
								.Include(x => x.Teacher)
									.ThenInclude(x => x.BanTeacherPeriodTimeslots)
										.ThenInclude(x => x.PeriodTimeslot)

								.Include(x => x.Course)
								.Include(x => x.LessonType)
								.Include(x => x.TeachingUnitClassroomTypes)
									.ThenInclude(x => x.ClassroomType)
								.ToListAsync();
				}

				return teachingUnits;
			});
		}

		public Task<TeachingUnit> UpdateAsync(TeachingUnit item)
		{
			throw new NotImplementedException();
		}
	}
}
