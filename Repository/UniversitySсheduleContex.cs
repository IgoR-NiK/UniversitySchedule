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
		public DbSet<Department> Departments { get; set; }
		public DbSet<Group> Groups { get; set; }

		public UniversitySсheduleContex(DbContextOptions<UniversitySсheduleContex> options) 
			: base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var faculty1 = new Faculty() { Id = 1, Name = "Факультет информационных технологий и управления", ShortName = "ФИТУ" };
			var faculty2 = new Faculty() { Id = 2, Name = "Строительный факультет", ShortName = "СФ" };
			var faculty3 = new Faculty() { Id = 3, Name = "Механический факультет", ShortName = "МФ" };
			var faculty4 = new Faculty() { Id = 4, Name = "Энергетический факультет", ShortName = "ЭНФ" };

			modelBuilder.Entity<Faculty>().HasData(faculty1, faculty2, faculty3, faculty4);
			
			var department1 = new Department() { Id = 1, Name = "Программное обеспечение вычислительной техники", FacultyId = faculty1.Id };
			var department2 = new Department() { Id = 2, Name = "Информационные технологии", FacultyId = faculty1.Id };

			modelBuilder.Entity<Department>().HasData(department1, department2);

			var group1 = new Group() { Id = 1, Name = "ФИТУ 2-4А", StudentsCount = 12, CoursesNumber = 2, DepartmentId = department2.Id };
			var group2 = new Group() { Id = 2, Name = "ФИТУ 2-4Б", StudentsCount = 20, CoursesNumber = 2, DepartmentId = department2.Id };
			var group3 = new Group() { Id = 3, Name = "ФИТУ 3-5", StudentsCount = 15, CoursesNumber = 3, DepartmentId = department1.Id };
			var group4 = new Group() { Id = 4, Name = "ФИТУ 3-5Б", StudentsCount = 25, CoursesNumber = 3, DepartmentId = department1.Id };

			modelBuilder.Entity<Group>().HasData(group1, group2, group3, group4);
		}
	}
}
