using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
	public interface IRepository<T> where T : class
	{
		Task<T> AddAsync(T item);

		Task<T> AddRangeAsync(IEnumerable<T> items);

		Task Clear();

		Task DeleteAsync(int id);

		Task<T> GetEntityAsync(int id);

		Task<List<T>> GetEntityListAsync();

		Task<T> UpdateAsync(T item);
	}
}
