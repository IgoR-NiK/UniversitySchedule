using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using UniversitySchedule.Models;
using UniversitySchedule.Converters;
using Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;

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
		[Authorize]
		public async Task<IEnumerable<Faculty>> Get()
		{
			var faculties = await FacultyRepository.GetEntityListAsync();
			return faculties.Select(f => FacultyConverter.Convert(f));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var faculty = await FacultyRepository.GetEntityAsync(id);
			
			if (faculty == null)
				return NotFound();

			return new OkObjectResult(FacultyConverter.Convert(faculty));
		}
	}
}