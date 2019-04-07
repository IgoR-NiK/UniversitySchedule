using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Repository.Entities;
using Repository.Interfaces;

namespace Repository.MsSqlRepository
{
	public class MsSqlWebPageRepository : MsSqlBaseRepository, IWebPageRepository
	{
		public MsSqlWebPageRepository(ConnectionOptions options) 
			: base(options) { }

		public Task<WebPage> AddAsync(WebPage item)
		{
			throw new NotImplementedException();
		}

		public Task AddRangeAsync(IEnumerable<WebPage> items)
		{
			throw new NotImplementedException();
		}

		public Task Clear()
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<string>> GetAllWebPagePathForRole(string roleName)
		{
			return Task.Run(() =>
			{
				var paths = new List<string>();

				using (var dbContext = GetDbContext())
				{
					paths = dbContext.Roles
								.Include(x => x.BanRoleWebPages)
								.ThenInclude(x => x.WebPage)
								.FirstOrDefault(x => x.Name == roleName)
								?.BanRoleWebPages.Select(x => x.WebPage.Path)
								.ToList() ?? paths;
				}

				return paths;
			});
		}

		public Task<WebPage> GetEntityAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<WebPage>> GetEntityListAsync()
		{
			throw new NotImplementedException();
		}

		public Task<WebPage> UpdateAsync(WebPage item)
		{
			throw new NotImplementedException();
		}
	}
}
