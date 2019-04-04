using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Таблица связка "Аудитория - Таймслот в планируемом периоде" для ограничения аудиторий в определенный таймслот.
	/// </summary>
	public class BanClassroomPeriodTimeslot
	{
		public int ClassroomId { get; set; }
		public Classroom Classroom { get; set; }

		public int PeriodTimeslotId { get; set; }
		public PeriodTimeslot PeriodTimeslot { get; set; }
	}
}
