using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models.Response;
using Microsoft.AspNetCore.SignalR;

namespace UniversitySchedule.Hubs
{
	public class ScheduleHub : Hub
	{
		public async Task SendScheduleInfo(ScheduleInfo info)
		{
			await Clients.All.SendAsync("SendScheduleInfo", info);
		}

		public async Task GenerateScheduleCompleted(GridResponse gridResponse)
		{
			await Clients.All.SendAsync("GenerateScheduleCompleted", gridResponse);
		}
	}
}
