using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Repository.Entities;

namespace Repository.Interfaces
{
	public interface IScheduleRepository : IRepository<ScheduleCell>
	{
		Task<List<PeriodTimeslot>> GetScheduleForGroupAsync(int groupId);
		Task<List<PeriodTimeslot>> GetScheduleForTeacherAsync(int teacherId);
	}
}
