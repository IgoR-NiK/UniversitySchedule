using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Response
{
	public class ClassroomResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Capacity { get; set; }
		public string ClassroomTypeName { get; set; }
		public string BuildingName { get; set; }
	}
}
