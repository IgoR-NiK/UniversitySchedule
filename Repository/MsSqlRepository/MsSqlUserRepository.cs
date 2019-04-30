using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Repository.Entities;
using Repository.Interfaces;

namespace Repository.MsSqlRepository
{
	public class MsSqlUserRepository : MsSqlBaseRepository, IUserRepository
	{
		public MsSqlUserRepository(ConnectionOptions options)
			: base(options) { }


		public Task<User> AddAsync(User item)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Users.Add(item);
					dbContext.SaveChanges();
				}

				return item;
			});
		}

		public Task AddRangeAsync(IEnumerable<User> items)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Users.AddRange(items);
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
					dbContext.Users.RemoveRange(dbContext.Users);
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
					var item = dbContext.Users.Find(id);
					if (item != null)
					{
						dbContext.Users.Remove(item);
						dbContext.SaveChanges();
					}
				}
			});
		}

		public Task<User> GetUserAsync(string login, string password)
		{
			return Task.Run(() =>
			{
				User user;

				using (var dbContext = GetDbContext())
				{
					var hash = MD5.Create().ComputeHash(Encoding.Default.GetBytes(password));
					user = dbContext.Users
								.Include(x => x.Role)
								.FirstOrDefault(x => x.Login == login && x.Password == hash);
				}

				return user;
			});
		}

		public Task<User> GetEntityAsync(int id)
		{
			return Task.Run(() =>
			{
				User user;

				using (var dbContext = GetDbContext())
				{
					user = dbContext.Users.FirstOrDefault(x => x.Id == id);
				}

				return user;
			});
		}

		public Task<List<User>> GetEntityListAsync()
		{
			return Task.Run(() =>
			{
				var users = new List<User>();

				using (var dbContext = GetDbContext())
				{
					users = dbContext.Users
								.Include(x => x.Role)
								.OrderBy(x => x.SecondName)
								.ToList();
				}

				return users;
			});
		}

		public Task<User> UpdateAsync(User item)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Users.Update(item);
					dbContext.SaveChanges();
				}

				return item;
			});
		}
	}
}
