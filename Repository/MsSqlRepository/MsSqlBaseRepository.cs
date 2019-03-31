using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace Repository.MsSqlRepository
{
	public abstract class MsSqlBaseRepository
	{
		public string ConnectionString { get; }

		public MsSqlBaseRepository(ConnectionOptions options)
		{
			ConnectionString = options.ConnectionString;
		}

		private protected UniversitySсheduleContex GetDbContext() =>
			new UniversitySсheduleContex(new DbContextOptionsBuilder<UniversitySсheduleContex>().UseSqlServer(ConnectionString).Options);
	}
}
