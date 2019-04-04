using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Таймслот в течении дня.
	/// </summary>
	public class DayTimeslot
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime StartTime { get; set; }

		public ICollection<PeriodTimeslot> PeriodTimeslots { get; set; } = new List<PeriodTimeslot>();
	}
}
