using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

using Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Enums;

namespace Repository
{
	class UniversitySсheduleContex : DbContext
	{
		public DbSet<Faculty> Faculties { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<WebPage> WebPages { get; set; }

		public UniversitySсheduleContex(DbContextOptions<UniversitySсheduleContex> options) 
			: base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			#region RoleWebPage

			modelBuilder.Entity<RoleWebPage>()
				.HasKey(x => new { x.RoleId, x.WebPageId });

			modelBuilder.Entity<RoleWebPage>()
				.HasOne(rwp => rwp.Role)
				.WithMany(r => r.RoleWebPages)
				.HasForeignKey(rwp => rwp.RoleId);

			modelBuilder.Entity<RoleWebPage>()
				.HasOne(rwp => rwp.WebPage)
				.WithMany(wp => wp.RoleWebPages)
				.HasForeignKey(rwp => rwp.WebPageId);

			#endregion


			#region Инициализация БД начальными значениями

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

			var role1 = new Role() { Id = 1, Name = "admin", Description = "Администратор веб-приложения" };
			var role2 = new Role() { Id = 2, Name = "methodist", Description = "Методист учебного отдела ВУЗа" };

			modelBuilder.Entity<Role>().HasData(role1, role2);

			var webPage1 = new WebPage() { Id = 1, Name = "Редактирование факультетов", Path = @"^(/administration/faculties)" };
			var webPage2 = new WebPage() { Id = 2, Name = "Редактирование кафедр", Path = @"^(/administration/departments)" };

			modelBuilder.Entity<WebPage>().HasData(webPage1, webPage2);

			var roleWebPage = new RoleWebPage() { RoleId = role2.Id, WebPageId = webPage2.Id };

			modelBuilder.Entity<RoleWebPage>().HasData(roleWebPage);

			var user1 = new User()
			{
				Id = 1,
				Login = "igor",
				Password = MD5.Create().ComputeHash(Encoding.Default.GetBytes("123")),
				FirstName = "Игорь",
				SecondName = "Николаев",
				MiddleName = "Германович",
				IsLocked = false,
				Gender = GenderType.Male,
				RoleId = role1.Id
			};

			var user2 = new User()
			{
				Id = 2,
				Login = "test",
				Password = MD5.Create().ComputeHash(Encoding.Default.GetBytes("123")),
				FirstName = "Пользователь",
				SecondName = "Тестовый",
				IsLocked = false,
				Gender = GenderType.Male,
				RoleId = role2.Id
			};

			modelBuilder.Entity<User>().HasData(user1, user2);

			#endregion
		}
	}
}
