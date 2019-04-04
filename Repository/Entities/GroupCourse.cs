using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Таблица связка "Учебная группа - Дисциплина".
	/// </summary>
	public class GroupCourse
	{
		public int GroupId { get; set; }
		public Group Group { get; set; }

		public int CourseId { get; set; }
		public Course Course { get; set; }
	}
}
