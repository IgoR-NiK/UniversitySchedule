using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Response
{
	public class WeekResponse
	{
		public string Name { get; set; }
		public List<DayResponse> DaysResponse { get; set; } = new List<DayResponse>();
	}
}
