using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Response
{
	public class DayTimeslotResponse
	{
		public int Number { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public string CourseName { get; set; }
		public string Classroom { get; set; }
		public string LessonTypeName { get; set; }

		public string TeacherName { get; set; }
		public string TeacherPost { get; set; }
		public string GroupName { get; set; }
	}
}
