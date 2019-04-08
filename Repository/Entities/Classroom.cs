using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Аудитория.
	/// </summary>
	public class Classroom
	{
		public int Id { get; set; }

		[StringLength(50)]
		public string Name { get; set; }

		public int Number { get; set; }

		public int Capacity { get; set; }

		public int? ClassroomTypeId { get; set; }
		public ClassroomType ClassroomType { get; set; }

		public int? BuildingId { get; set; }
		public Building Building { get; set; }

		public ICollection<BanClassroomPeriodTimeslot> BanClassroomPeriodTimeslots { get; set; } = new List<BanClassroomPeriodTimeslot>();
		public ICollection<ScheduleCell> ScheduleCells { get; set; } = new List<ScheduleCell>();
	}
}
