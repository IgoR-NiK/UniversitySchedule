using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Repository.Interfaces;

namespace UniversitySchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
		IFacultyRepository FacultyRepository { get; }

		public CoursesController(IFacultyRepository facultyRepository)
		{
			FacultyRepository = facultyRepository;
		}

		[HttpGet]
		public async Task<IEnumerable<int>> Get(int facultyId)
		{
			return await FacultyRepository.GetCoursesForFacultyAsync(facultyId);
		}
	}
}