using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Response
{
	public class BuildingResponse
	{
		public string Name { get; set; }
		public List<ClassroomResponse> ClassroomsResponse { get; set; } = new List<ClassroomResponse>();
	}
}
