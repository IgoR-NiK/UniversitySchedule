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
	public class MsSqlScheduleRepository : MsSqlBaseRepository, IScheduleRepository
	{
		public MsSqlScheduleRepository(ConnectionOptions options) 
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
			throw new NotImplementedException();
		}

		public Task<List<PeriodTimeslot>> GetScheduleForGroupAsync(int groupId)
		{
			return Task.Run(() =>
			{
				var periodTimeslots = new List<PeriodTimeslot>();

				using (var dbContext = GetDbContext())
				{
					var parentGroupId = dbContext.Groups
											.Find(groupId)
											?.ParentGroupId ?? 0;

					periodTimeslots = dbContext.PeriodTimeslots
											.Include(x => x.Week)
											.Include(x => x.Day)
											.Include(x => x.DayTimeslot)
											.GroupJoin(dbContext.ScheduleCells
															.Include(x => x.Classroom)
															.Include(x => x.TeachingUnit)
																.ThenInclude(x => x.Course)
															.Include(x => x.TeachingUnit)
																.ThenInclude(x => x.LessonType)
															.Include(x => x.TeachingUnit)
																.ThenInclude(x => x.Group)
															.Include(x => x.TeachingUnit)
																.ThenInclude(x => x.Teacher)
																.ThenInclude(x => x.Post)
															.Where(x => x.TeachingUnit.GroupId == groupId ||
																		x.TeachingUnit.GroupId == parentGroupId)
															.ToList(),
														pt => pt.Id,
														t => t.PeriodTimeslotId,
														(pt, t) => pt)
											.ToList();
				}
				
				return periodTimeslots;
			});
		}

		public Task<List<PeriodTimeslot>> GetScheduleForTeacherAsync(int teacherId)
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
											.GroupJoin(dbContext.ScheduleCells
															.Include(x => x.Classroom)
															.Include(x => x.TeachingUnit)
																.ThenInclude(x => x.Course)
															.Include(x => x.TeachingUnit)
																.ThenInclude(x => x.LessonType)
															.Include(x => x.TeachingUnit)
																.ThenInclude(x => x.Group)
															.Include(x => x.TeachingUnit)
																.ThenInclude(x => x.Teacher)
																.ThenInclude(x => x.Post)
															.Where(x => x.TeachingUnit.TeacherId == teacherId)
															.ToList(),
														pt => pt.Id,
														t => t.PeriodTimeslotId,
														(pt, t) => pt)
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
