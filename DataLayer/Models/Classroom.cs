using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
	public class Classroom
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Number { get; set; }
		public int Capacity { get; set; }

		public int? ClassroomTypeId { get; set; }
		public ClassroomType ClassroomType { get; set; }

		public int? BuildingId { get; set; }
		public Building Building { get; set; }

		public List<PeriodTimeslot> BanPeriodTimeslots { get; set; } = new List<PeriodTimeslot>();

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object obj)
		{
			return obj is Classroom classroom ? Id == classroom.Id : false;
		}
	}
}
