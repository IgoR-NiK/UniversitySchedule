using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
	public interface IRepository<T> where T : class
	{
		Task AddAsync(T item);		

		Task DeleteAsync(int id);

		Task<T> GetEntityAsync(int id);

		Task<List<T>> GetEntityListAsync();

		Task UpdateAsync(T item);
	}
}
