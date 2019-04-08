using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Учебная единица.
	/// </summary>
	public class TeachingUnit
	{
		public int Id { get; set; }

		public int CountInPeriodTimeslot { get; set; }

		public int GroupId { get; set; }
		public Group Group { get; set; }

		public int TeacherId { get; set; }
		public Teacher Teacher { get; set; }

		public int CourseId { get; set; }
		public Course Course { get; set; }

		public int LessonTypeId { get; set; }
		public LessonType LessonType { get; set; }

		public ICollection<TeachingUnitClassroomType> TeachingUnitClassroomTypes { get; set; } = new List<TeachingUnitClassroomType>();
		public ICollection<ScheduleCell> ScheduleCells { get; set; } = new List<ScheduleCell>();
	}
}
