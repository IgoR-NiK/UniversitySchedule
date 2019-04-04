using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Таблица связка "Преподаватель - Таймслот в планируемом периоде" для ограничения преподавателя в определенный таймслот.
	/// </summary>
	public class BanTeacherPeriodTimeslot
	{
		public int TeacherId { get; set; }
		public Teacher Teacher { get; set; }

		public int PeriodTimeslotId { get; set; }
		public PeriodTimeslot PeriodTimeslot { get; set; }
	}
}
