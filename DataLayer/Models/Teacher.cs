using System;
using System.Collections.Generic;
using System.Text;

using Repository.Enums;

namespace DataLayer.Models
{
	public class Teacher
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string MiddleName { get; set; }
		public GenderType Gender { get; set; }
		public DateTime Birthday { get; set; }

		public int? DepartmentId { get; set; }
		public Department Department { get; set; }

		public int? PostId { get; set; }
		public Post Post { get; set; }

		public List<PeriodTimeslot> BanPeriodTimeslots { get; set; } = new List<PeriodTimeslot>();

		public override string ToString()
		{
			return $"{SecondName} {FirstName}" + (String.IsNullOrWhiteSpace(MiddleName) ? "" : $" {MiddleName}");
		}

		public override bool Equals(object obj)
		{
			return obj is Teacher teacher ? Id == teacher.Id : false;
		}
	}
}
