using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Response
{
	public class ScheduleInfo
	{
		/// <summary>
		/// Максимальная оценка расписания.
		/// </summary>
		public double MaxValue { get; set; }

		/// <summary>
		/// Средняя оценка расписаний.
		/// </summary>
		public double AverageValue { get; set; }

		/// <summary>
		/// Средний возраст решений (расписаний) в пуле.
		/// </summary>
		public double AverageAge { get; set; }
	}
}
