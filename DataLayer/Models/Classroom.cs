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

		public List<int> BanPeriodTimeslots { get; set; } = new List<int>();
	}
}
