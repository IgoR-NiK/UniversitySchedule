using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using DataLayer.Models;
using DataLayer.Converters;
using Repository.Interfaces;

namespace UniversitySchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
		IGroupRepository GroupRepository { get; }

		public GroupsController(IGroupRepository groupRepository)
		{
			GroupRepository = groupRepository;
		}


		[HttpGet]
		public async Task<IEnumerable<Group>> Get(int facultyId, int courseNumber)
		{
			var groups = await GroupRepository.GetGroupsForFacultyAndCourseAsync(facultyId, courseNumber);
			return groups.Select(x => GroupConverter.Convert(x));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var group = await GroupRepository.GetEntityAsync(id);

			if (group == null)
				return NotFound();

			return new OkObjectResult(GroupConverter.Convert(group));
		}
	}
}
