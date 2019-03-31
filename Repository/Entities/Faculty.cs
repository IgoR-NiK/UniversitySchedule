using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
	public class Faculty
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ShortName { get; set; }
		
		public ICollection<Department> Departments { get; set; } = new List<Department>();
	}
}
