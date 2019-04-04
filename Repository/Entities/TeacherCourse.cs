using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Таблица связка "Преподаватель - Дисциплина".
	/// </summary>
	public class TeacherCourse
	{
		public int TeacherId { get; set; }
		public Teacher Teacher { get; set; }

		public int CourseId { get; set; }
		public Course Course { get; set; }
	}
}
