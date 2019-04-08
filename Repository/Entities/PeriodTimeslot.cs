using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Таймслот в планируемом периоде.
	/// </summary>
	public class PeriodTimeslot
	{
		public int Id { get; set; }

		public int WeekId { get; set; }
		public Week Week { get; set; }

		public int DayId { get; set; }
		public Day Day { get; set; }

		public int DayTimeslotId { get; set; }
		public DayTimeslot DayTimeslot { get; set; }

		public ICollection<BanClassroomPeriodTimeslot> BanClassroomPeriodTimeslots { get; set; } = new List<BanClassroomPeriodTimeslot>();
		public ICollection<BanTeacherPeriodTimeslot> BanTeacherPeriodTimeslots { get; set; } = new List<BanTeacherPeriodTimeslot>();
		public ICollection<ScheduleCell> ScheduleCells { get; set; } = new List<ScheduleCell>();
	}
}
