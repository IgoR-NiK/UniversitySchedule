using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models.Response
{
	public class GridResponse
	{
		public List<WeekResponse> Weeks { get; set; } = new List<WeekResponse>();
		public List<BuildingResponse> Buildings { get; set; } = new List<BuildingResponse>();
		public List<List<ScheduleCellResponse>> Cells { get; set; } = new List<List<ScheduleCellResponse>>();
	}
}
