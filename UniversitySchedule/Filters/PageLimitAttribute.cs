using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Filters;
using Repository.Interfaces;

namespace UniversitySchedule.Filters
{
	public class PageLimitAttribute : Attribute, IAsyncResultFilter
	{
		IWebPageRepository WebPageRepository { get; }

		public PageLimitAttribute(IWebPageRepository webPageRepository)
		{
			WebPageRepository = webPageRepository;
		}

		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			var roleName = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value ?? String.Empty;
			var paths = await WebPageRepository.GetAllWebPagePathForRole(roleName);

			var currentPath = context.HttpContext.Request.Query["path"].First();

			if (paths.Any(x => Regex.IsMatch(currentPath, x)))
			{
				context.Result = new ForbidResult();
			}

			await next();
		}
	}
}
