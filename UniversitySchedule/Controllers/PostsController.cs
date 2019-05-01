using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using DataLayer.Models;
using DataLayer.Converters;
using Repository.Interfaces;
using DataLayer.Models.Response;

namespace UniversitySchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
		IPostRepository PostRepository { get; }

		public PostsController(IPostRepository postRepository)
		{
			PostRepository = postRepository;
		}


		[HttpGet]
		[Route("GetAdminPosts")]
		public async Task<IEnumerable<PostResponse>> GetAdminPosts()
		{
			var posts = await PostRepository.GetEntityListAsync();
			return posts.Select(x => PostConverter.ConvertTo(x));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Post post)
		{
			if (post == null)
				return BadRequest();

			await PostRepository.AddAsync(PostConverter.Convert(post));
			return new OkObjectResult(post);
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Post post)
		{
			if (post == null)
				return BadRequest();

			await PostRepository.UpdateAsync(PostConverter.Convert(post));
			return new OkObjectResult(post);
		}

		[HttpDelete]
		public async Task Delete(int id)
		{
			await PostRepository.DeleteAsync(id);
		}
	}
}