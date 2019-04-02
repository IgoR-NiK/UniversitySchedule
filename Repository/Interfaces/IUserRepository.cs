using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Repository.Entities;

namespace Repository.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User> GetUserAsync(string login, string password);
	}
}
