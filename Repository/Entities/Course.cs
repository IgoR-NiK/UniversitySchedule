using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Дисциплина.
	/// </summary>
	public class Course
	{
		public int Id { get; set; }

		[Required]
		[StringLength(300)]
		public string Name { get; set; }

		public ICollection<TeacherCourse> TeacherCourses { get; set; } = new List<TeacherCourse>();
		public ICollection<GroupCourse> GroupCourses { get; set; } = new List<GroupCourse>();
		public ICollection<TeachingUnit> TeachingUnits { get; set; } = new List<TeachingUnit>();
	}
}
