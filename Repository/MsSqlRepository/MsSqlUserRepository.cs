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
			throw new NotImplementedException();
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
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
			throw new NotImplementedException();
		}

		public Task<List<User>> GetEntityListAsync()
		{
			throw new NotImplementedException();
		}

		public Task<User> UpdateAsync(User item)
		{
			throw new NotImplementedException();
		}
	}
}
