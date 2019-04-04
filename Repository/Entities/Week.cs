using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Учебная неделя.
	/// </summary>
	public class Week
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public ICollection<PeriodTimeslot> PeriodTimeslots { get; set; } = new List<PeriodTimeslot>();
	}
}
