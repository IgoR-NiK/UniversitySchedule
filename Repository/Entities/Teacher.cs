using Repository.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Преподаватель.
	/// </summary>
	public class Teacher
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string FirstName { get; set; }

		[Required]
		[StringLength(100)]
		public string SecondName { get; set; }

		[StringLength(100)]
		public string MiddleName { get; set; }

		public GenderType Gender { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime Birthday { get; set; }

		public int? DepartmentId { get; set; }
		public Department Department { get; set; }

		public int? PostId { get; set; }
		public Post Post { get; set; }

		public ICollection<BanTeacherPeriodTimeslot> BanTeacherPeriodTimeslots { get; set; } = new List<BanTeacherPeriodTimeslot>();
		public ICollection<TeacherCourse> TeacherCourses { get; set; } = new List<TeacherCourse>();
		public ICollection<TeachingUnit> TeachingUnits { get; set; } = new List<TeachingUnit>();
	}
}
