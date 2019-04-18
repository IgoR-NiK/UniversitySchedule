using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Response
{
	public class DayResponse
	{
		public string Name { get; set; }
		public string Date { get; set; }
		public List<DayTimeslotResponse> DayTimeslotsResponse { get; set; } = new List<DayTimeslotResponse>();
	}
}
