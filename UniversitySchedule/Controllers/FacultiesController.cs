using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Repository.Entities;
using Repository.Interfaces;

namespace UniversitySchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
		IFacultyRepository FacultyRepository { get; }

		public FacultiesController(IFacultyRepository facultyRepository)
		{
			FacultyRepository = facultyRepository;
		}

		[HttpGet]
		public async Task<IEnumerable<Faculty>> Get()
		{
			return await FacultyRepository.GetEntityListAsync();
		}
	}
}