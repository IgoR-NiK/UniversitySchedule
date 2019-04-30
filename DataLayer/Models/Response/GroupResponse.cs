using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Response
{
	public class GroupResponse
	{
		public string Name { get; set; }
		public int StudentCount { get; set; }
		public int NumberCourse { get; set; }
		public string DepartmentName { get; set; }
	}
}
