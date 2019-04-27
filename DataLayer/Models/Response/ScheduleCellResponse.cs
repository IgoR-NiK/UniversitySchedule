using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models.Enums;

namespace DataLayer.Models.Response
{
	public class ScheduleCellResponse
	{
		public string CourseName { get; set; }
		public string LessonTypeName { get; set; }
		public string GroupName { get; set; }
		public string TeacherPost { get; set; }
		public string TeacherName { get; set; }

		public CellType CellType { get; set; }
	}
}
