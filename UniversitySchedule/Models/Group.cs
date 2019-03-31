using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversitySchedule.Models
{
	public class Group
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int CoursesNumber { get; set; }
		public int StudentsCount { get; set; }
	}
}
