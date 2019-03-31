using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using UniversitySchedule.Models;
using UniversitySchedule.Converters;
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
    }
}
