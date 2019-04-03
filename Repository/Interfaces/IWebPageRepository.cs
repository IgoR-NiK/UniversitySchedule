using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Repository.Entities;

namespace Repository.Interfaces
{
	public interface IWebPageRepository : IRepository<WebPage>
	{
		Task<List<string>> GetAllWebPagePathForRole(string roleName);
	}
}
