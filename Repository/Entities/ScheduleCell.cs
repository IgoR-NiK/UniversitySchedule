using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Ячейка расписания.
	/// </summary>
	public class ScheduleCell
	{
		public int ClassroomId { get; set; }
		public Classroom Classroom { get; set; }

		public int PeriodTimeslotId { get; set; }
		public PeriodTimeslot PeriodTimeslot { get; set; }

		public int TeachingUnitId { get; set; }
		public TeachingUnit TeachingUnit { get; set; }
	}
}
