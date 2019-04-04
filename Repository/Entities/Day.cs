using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// День недели.
	/// </summary>
	public class Day
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public ICollection<PeriodTimeslot> PeriodTimeslots { get; set; } = new List<PeriodTimeslot>();
	}
}
