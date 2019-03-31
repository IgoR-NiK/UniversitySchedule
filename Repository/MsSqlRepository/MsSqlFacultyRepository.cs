using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Repository.Entities;
using Repository.Interfaces;

namespace Repository.MsSqlRepository
{
	public class MsSqlFacultyRepository : MsSqlBaseRepository, IFacultyRepository
	{
		public MsSqlFacultyRepository(ConnectionOptions options) 
			: base(options) { }

		public Task<Faculty> AddAsync(Faculty item)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Faculty> GetEntityAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<Faculty>> GetEntityListAsync()
		{
			return Task.Run(() =>
			{
				var faculties = new List<Faculty>();

				using(var dbContext = GetDbContext())
				{
					faculties = dbContext.Faculties.OrderBy(f => f.Name).ToList();
				}

				return faculties;
			});
		}

		public Task<Faculty> UpdateAsync(Faculty item)
		{
			throw new NotImplementedException();
		}
	}
}
