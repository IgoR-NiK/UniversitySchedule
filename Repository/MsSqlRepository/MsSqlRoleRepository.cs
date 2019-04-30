using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.MsSqlRepository
{
	public class MsSqlRoleRepository : MsSqlBaseRepository, IRoleRepository
	{
		public MsSqlRoleRepository(ConnectionOptions options) 
			: base(options) { }

		public Task<Role> AddAsync(Role item)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Roles.Add(item);
					dbContext.SaveChanges();
				}

				return item;
			});
		}

		public Task AddRangeAsync(IEnumerable<Role> items)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Roles.AddRange(items);
					dbContext.SaveChanges();
				}
			});
		}

		public Task Clear()
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Roles.RemoveRange(dbContext.Roles);
					dbContext.SaveChanges();
				}
			});
		}

		public Task DeleteAsync(int id)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					var item = dbContext.Roles.Find(id);
					if (item != null)
					{
						dbContext.Roles.Remove(item);
						dbContext.SaveChanges();
					}
				}
			});
		}

		public Task<Role> GetEntityAsync(int id)
		{
			return Task.Run(() =>
			{
				Role role;

				using (var dbContext = GetDbContext())
				{
					role = dbContext.Roles.FirstOrDefault(x => x.Id == id);
				}

				return role;
			});
		}

		public Task<List<Role>> GetEntityListAsync()
		{
			return Task.Run(() =>
			{
				var roles = new List<Role>();

				using (var dbContext = GetDbContext())
				{
					roles = dbContext.Roles.OrderBy(x => x.Name).ToList();
				}

				return roles;
			});
		}

		public Task<Role> UpdateAsync(Role item)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Roles.Update(item);
					dbContext.SaveChanges();
				}

				return item;
			});
		}
	}
}
