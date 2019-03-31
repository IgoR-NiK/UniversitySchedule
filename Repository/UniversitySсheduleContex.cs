using System;
using System.Collections.Generic;
using System.Text;

using Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	class UniversitySсheduleContex : DbContext
	{
		public DbSet<Faculty> Faculties { get; set; }

		public UniversitySсheduleContex(DbContextOptions<UniversitySсheduleContex> options) 
			: base(options)
		{
			Database.EnsureCreated();
		}
	}
}
