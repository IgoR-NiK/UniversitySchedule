using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

using Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using System.Linq;

namespace Repository
{
	class UniversitySсheduleContex : DbContext
	{
		public DbSet<Building> Buildings { get; set; }
		public DbSet<Classroom> Classrooms { get; set; }
		public DbSet<ClassroomType> ClassroomTypes { get; set; }

		public DbSet<Week> Weeks { get; set; }
		public DbSet<Day> Days { get; set; }
		public DbSet<DayTimeslot> DayTimeslots { get; set; }
		public DbSet<PeriodTimeslot> PeriodTimeslots { get; set; }
				
		public DbSet<Faculty> Faculties { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Group> Groups { get; set; }

		public DbSet<Course> Courses { get; set; }
		public DbSet<LessonType> LessonTypes { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Teacher> Teachers { get; set; }

		public DbSet<TeachingUnit> TeachingUnits { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		
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
			#region BanRoleWebPage

			modelBuilder.Entity<BanRoleWebPage>()
				.HasKey(x => new { x.RoleId, x.WebPageId });

			modelBuilder.Entity<BanRoleWebPage>()
				.HasOne(brwp => brwp.Role)
				.WithMany(r => r.BanRoleWebPages)
				.HasForeignKey(brwp => brwp.RoleId);

			modelBuilder.Entity<BanRoleWebPage>()
				.HasOne(brwp => brwp.WebPage)
				.WithMany(wp => wp.BanRoleWebPages)
				.HasForeignKey(brwp => brwp.WebPageId);

			#endregion

			#region BanClassroomPeriodTimeslot

			modelBuilder.Entity<BanClassroomPeriodTimeslot>()
				.HasKey(x => new { x.ClassroomId, x.PeriodTimeslotId });

			modelBuilder.Entity<BanClassroomPeriodTimeslot>()
				.HasOne(bcpt => bcpt.Classroom)
				.WithMany(c => c.BanClassroomPeriodTimeslots)
				.HasForeignKey(bcpt => bcpt.ClassroomId);

			modelBuilder.Entity<BanClassroomPeriodTimeslot>()
				.HasOne(bcpt => bcpt.PeriodTimeslot)
				.WithMany(c => c.BanClassroomPeriodTimeslots)
				.HasForeignKey(bcpt => bcpt.PeriodTimeslotId);

			#endregion

			#region BanTeacherPeriodTimeslot

			modelBuilder.Entity<BanTeacherPeriodTimeslot>()
				.HasKey(x => new { x.TeacherId, x.PeriodTimeslotId });

			modelBuilder.Entity<BanTeacherPeriodTimeslot>()
				.HasOne(btpt => btpt.Teacher)
				.WithMany(c => c.BanTeacherPeriodTimeslots)
				.HasForeignKey(btpt => btpt.TeacherId);

			modelBuilder.Entity<BanTeacherPeriodTimeslot>()
				.HasOne(btpt => btpt.PeriodTimeslot)
				.WithMany(c => c.BanTeacherPeriodTimeslots)
				.HasForeignKey(btpt => btpt.PeriodTimeslotId);

			#endregion

			#region TeacherCourse

			modelBuilder.Entity<TeacherCourse>()
				.HasKey(x => new { x.TeacherId, x.CourseId });

			modelBuilder.Entity<TeacherCourse>()
				.HasOne(tc => tc.Teacher)
				.WithMany(t => t.TeacherCourses)
				.HasForeignKey(tc => tc.TeacherId);

			modelBuilder.Entity<TeacherCourse>()
				.HasOne(tc => tc.Course)
				.WithMany(c => c.TeacherCourses)
				.HasForeignKey(tc => tc.CourseId);

			#endregion

			#region GroupCourse

			modelBuilder.Entity<GroupCourse>()
				.HasKey(x => new { x.GroupId, x.CourseId });

			modelBuilder.Entity<GroupCourse>()
				.HasOne(gc => gc.Group)
				.WithMany(g => g.GroupCourses)
				.HasForeignKey(gc => gc.GroupId);

			modelBuilder.Entity<GroupCourse>()
				.HasOne(gc => gc.Course)
				.WithMany(c => c.GroupCourses)
				.HasForeignKey(gc => gc.CourseId);

			#endregion

			#region TeachingUnitClassroomType

			modelBuilder.Entity<TeachingUnitClassroomType>()
				.HasKey(x => new { x.TeachingUnitId, x.ClassroomTypeId });

			modelBuilder.Entity<TeachingUnitClassroomType>()
				.HasOne(tuct => tuct.TeachingUnit)
				.WithMany(tu => tu.TeachingUnitClassroomTypes)
				.HasForeignKey(tuct => tuct.TeachingUnitId);

			modelBuilder.Entity<TeachingUnitClassroomType>()
				.HasOne(tuct => tuct.ClassroomType)
				.WithMany(ct => ct.TeachingUnitClassroomTypes)
				.HasForeignKey(tuct => tuct.ClassroomTypeId);

			#endregion

			#region Schedule

			modelBuilder.Entity<Schedule>()
				.HasKey(x => new { x.ClassroomId, x.PeriodTimeslotId });

			#endregion


			#region Инициализация БД начальными значениями

			#region Недели

			var week1 = new Week() { Id = 1, Name = "1 неделя" };

			modelBuilder.Entity<Week>().HasData(week1);

			#endregion

			#region Дни недели

			var day1 = new Day() { Id = 1, Name = "Понедельник" };
			var day2 = new Day() { Id = 2, Name = "Вторник" };
			var day3 = new Day() { Id = 3, Name = "Четверг" };
			var day4 = new Day() { Id = 4, Name = "Пятница" };

			modelBuilder.Entity<Day>().HasData(day1, day2, day3, day4);

			#endregion

			#region Таймслоты в течении дня

			var dayTimeslot1 = new DayTimeslot() { Id = 1, Name = "1 пара", StartTime = new DateTime(1, 1, 1, 9, 0, 0) };
			var dayTimeslot2 = new DayTimeslot() { Id = 2, Name = "2 пара", StartTime = new DateTime(1, 1, 1, 10, 45, 0) };
			var dayTimeslot3 = new DayTimeslot() { Id = 3, Name = "3 пара", StartTime = new DateTime(1, 1, 1, 12, 45, 0) };
			var dayTimeslot4 = new DayTimeslot() { Id = 4, Name = "4 пара", StartTime = new DateTime(1, 1, 1, 14, 30, 0) };

			modelBuilder.Entity<DayTimeslot>().HasData(dayTimeslot1, dayTimeslot2, dayTimeslot3, dayTimeslot4);

			#endregion

			#region Таймслоты в планируемом периоде

			var periodTimeslots = new PeriodTimeslot[16];
			for (var i = 1; i <= 16; i++)
				periodTimeslots[i - 1] = new PeriodTimeslot() { Id = i, WeekId = week1.Id, DayId = (i - 1) / 4 + 1, DayTimeslotId = (i - 1) % 4 + 1 };

			modelBuilder.Entity<PeriodTimeslot>().HasData(periodTimeslots);

			#endregion

			#region Учебные корпуса

			var building1 = new Building() { Id = 1, Name = "Главный корпус", ShortName = "ГЛ" };
			var building2 = new Building() { Id = 2, Name = "Лабораторный корпус", ShortName = "ЛК" };

			modelBuilder.Entity<Building>().HasData(building1, building2);

			#endregion

			#region Типы аудиторий по назначению

			var classroomType1 = new ClassroomType() { Id = 1, Name = "Лекционная аудитория" };
			var classroomType2 = new ClassroomType() { Id = 2, Name = "Практическая аудитория" };
			var classroomType3 = new ClassroomType() { Id = 3, Name = "Компьютерная аудитория" };

			modelBuilder.Entity<ClassroomType>().HasData(classroomType1, classroomType2, classroomType3);

			#endregion

			#region Аудитории

			var classroom1 = new Classroom() { Id = 1, Name = "ГЛ 1", Number = 1, Capacity = 25, ClassroomTypeId = classroomType1.Id, BuildingId = building1.Id };
			var classroom2 = new Classroom() { Id = 2, Name = "ГЛ 2", Number = 2, Capacity = 25, ClassroomTypeId = classroomType2.Id, BuildingId = building1.Id };
			var classroom3 = new Classroom() { Id = 3, Name = "ГЛ 3", Number = 3, Capacity = 50, ClassroomTypeId = classroomType2.Id, BuildingId = building1.Id };
			var classroom4 = new Classroom() { Id = 4, Name = "ГЛ 4", Number = 4, Capacity = 25, ClassroomTypeId = classroomType3.Id, BuildingId = building1.Id };

			var classroom5 = new Classroom() { Id = 5, Name = "ЛК 1", Number = 1, Capacity = 50, ClassroomTypeId = classroomType1.Id, BuildingId = building2.Id };
			var classroom6 = new Classroom() { Id = 6, Name = "ЛК 2", Number = 2, Capacity = 50, ClassroomTypeId = classroomType3.Id, BuildingId = building2.Id };

			modelBuilder.Entity<Classroom>().HasData(classroom1, classroom2, classroom3, classroom4, classroom5, classroom6);

			#endregion

			#region Ограничения "Аудитория - Таймслот в планируемом периоде"

			var banClassrooms = new List<(int classroomId, int periodTimeslotId)>()
			{
				(1, 4), (1, 7), (1, 8),
				(2, 5), (2, 6), (2, 15), (2, 16),
				(3, 2), (3, 3), (3, 10), (3, 11),
				(4, 8), (4, 9),
				(5, 9), (5, 10),
				(6, 13), (6, 14)
			}.Select(x => new BanClassroomPeriodTimeslot() { ClassroomId = x.classroomId, PeriodTimeslotId = x.periodTimeslotId });

			modelBuilder.Entity<BanClassroomPeriodTimeslot>().HasData(banClassrooms);

			#endregion					   			 

			#region Факультеты

			var faculty1 = new Faculty() { Id = 1, Name = "Факультет информационных технологий и управления", ShortName = "ФИТУ" };
			var faculty2 = new Faculty() { Id = 2, Name = "Строительный факультет", ShortName = "СФ" };
			var faculty3 = new Faculty() { Id = 3, Name = "Механический факультет", ShortName = "МФ" };
			var faculty4 = new Faculty() { Id = 4, Name = "Энергетический факультет", ShortName = "ЭНФ" };

			modelBuilder.Entity<Faculty>().HasData(faculty1, faculty2, faculty3, faculty4);

			#endregion

			#region Кафедры

			var department1 = new Department() { Id = 1, Name = "Программное обеспечение вычислительной техники", FacultyId = faculty1.Id };
			var department2 = new Department() { Id = 2, Name = "Информационные технологии", FacultyId = faculty1.Id };

			var department3 = new Department() { Id = 3, Name = "Кафедра СФ 1", FacultyId = faculty2.Id };
			var department4 = new Department() { Id = 4, Name = "Кафедра СФ 2", FacultyId = faculty2.Id };

			var department5 = new Department() { Id = 5, Name = "Кафедра МФ 1", FacultyId = faculty3.Id };
			var department6 = new Department() { Id = 6, Name = "Кафедра МФ 2", FacultyId = faculty3.Id };

			var department7 = new Department() { Id = 7, Name = "Кафедра ЭНФ 1", FacultyId = faculty4.Id };
			var department8 = new Department() { Id = 8, Name = "Кафедра ЭНФ 2", FacultyId = faculty4.Id };

			modelBuilder.Entity<Department>().HasData(department1, department2, department3, department4, department5, department6, department7, department8);

			#endregion

			#region Учебные группы

			var group1 = new Group() { Id = 1, Name = "ФИТУ 1-1, 1-2", StudentsCount = 47, CoursesNumber = 1 };
			var group2 = new Group() { Id = 2, Name = "ФИТУ 1-1", StudentsCount = 20, CoursesNumber = 1, ParentGroupId = 1, DepartmentId = department1.Id };
			var group3 = new Group() { Id = 3, Name = "ФИТУ 1-2", StudentsCount = 27, CoursesNumber = 1, ParentGroupId = 1, DepartmentId = department2.Id };

			var group4 = new Group() { Id = 4, Name = "ФИТУ 2-1, 2-2", StudentsCount = 47, CoursesNumber = 1 };
			var group5 = new Group() { Id = 5, Name = "ФИТУ 2-1", StudentsCount = 20, CoursesNumber = 2, ParentGroupId = 4, DepartmentId = department1.Id };
			var group6 = new Group() { Id = 6, Name = "ФИТУ 2-2", StudentsCount = 27, CoursesNumber = 2, ParentGroupId = 5, DepartmentId = department2.Id };

			var group7 = new Group() { Id = 7, Name = "ФИТУ 3-1, 3-2", StudentsCount = 47, CoursesNumber = 1 };
			var group8 = new Group() { Id = 8, Name = "ФИТУ 3-1", StudentsCount = 20, CoursesNumber = 3, ParentGroupId = 7, DepartmentId = department1.Id };
			var group9 = new Group() { Id = 9, Name = "ФИТУ 3-2", StudentsCount = 27, CoursesNumber = 3, ParentGroupId = 7, DepartmentId = department2.Id };

			var group10 = new Group() { Id = 10, Name = "ФИТУ 4-1, 4-2", StudentsCount = 47, CoursesNumber = 1 };
			var group11 = new Group() { Id = 11, Name = "ФИТУ 4-1", StudentsCount = 20, CoursesNumber = 4, ParentGroupId = 10, DepartmentId = department1.Id };
			var group12 = new Group() { Id = 12, Name = "ФИТУ 4-2", StudentsCount = 27, CoursesNumber = 4, ParentGroupId = 10, DepartmentId = department2.Id };
		
			modelBuilder.Entity<Group>().HasData(group1, group2, group3, group4, group5, group6, group7, group8, group9, group10, group11, group12);

			#endregion

			#region Должности преподавателей

			var post1 = new Post() { Id = 11, Name = "Ассистент" };
			var post2 = new Post() { Id = 2, Name = "Cтарший преподаватель" };
			var post3 = new Post() { Id = 3, Name = "Доцент" };
			var post4 = new Post() { Id = 4, Name = "Профессор" };

			modelBuilder.Entity<Post>().HasData(post1, post2, post3, post4);

			#endregion

			#region Преподаватели

			var teacher1 = new Teacher() { Id = 1, FirstName = "Преподаватель 1", SecondName = "Тестовый", PostId = post1.Id, DepartmentId = department1.Id, Gender = GenderType.Male, Birthday = new DateTime(1972, 7, 18) };
			var teacher2 = new Teacher() { Id = 2, FirstName = "Преподаватель 2", SecondName = "Тестовый", PostId = post2.Id, DepartmentId = department1.Id, Gender = GenderType.Male, Birthday = new DateTime(1994, 9, 3) };
			var teacher3 = new Teacher() { Id = 3, FirstName = "Преподаватель 3", SecondName = "Тестовый", PostId = post3.Id, DepartmentId = department2.Id, Gender = GenderType.Female, Birthday = new DateTime(1984, 11, 29) };
			var teacher4 = new Teacher() { Id = 4, FirstName = "Преподаватель 4", SecondName = "Тестовый", PostId = post4.Id, DepartmentId = department2.Id, Gender = GenderType.Male, Birthday = new DateTime(1978, 2, 6) };

			modelBuilder.Entity<Teacher>().HasData(teacher1, teacher2, teacher3, teacher4);

			#endregion

			#region Ограничения "Преподаватель - Таймслот в планируемом периоде"

			var banTeachers = new List<(int teacherId, int periodTimeslotId)>()
			{
				(1, 1), (1, 2), (1, 3), (1, 4),
				(2, 5), (2, 6), (2, 7), (2, 8),
				(3, 9), (3, 10), (3, 11), (3, 12),
				(4, 13), (4, 14), (4, 15), (4, 16)
			}.Select(x => new BanTeacherPeriodTimeslot() { TeacherId = x.teacherId, PeriodTimeslotId = x.periodTimeslotId });

			modelBuilder.Entity<BanTeacherPeriodTimeslot>().HasData(banTeachers);

			#endregion

			#region Дисциплины

			var course1 = new Course() { Id = 1, Name = "Дисциплина 1" };
			var course2 = new Course() { Id = 2, Name = "Дисциплина 2" };
			var course3 = new Course() { Id = 3, Name = "Дисциплина 3" };
			var course4 = new Course() { Id = 4, Name = "Дисциплина 4" };

			modelBuilder.Entity<Course>().HasData(course1, course2, course3, course4);

			#endregion

			#region Таблица связка "Преподаватель - Дисциплина"

			var teacherCourse1 = new TeacherCourse() { TeacherId = teacher1.Id, CourseId = course1.Id };
			var teacherCourse2 = new TeacherCourse() { TeacherId = teacher2.Id, CourseId = course2.Id };
			var teacherCourse3 = new TeacherCourse() { TeacherId = teacher3.Id, CourseId = course3.Id };
			var teacherCourse4 = new TeacherCourse() { TeacherId = teacher4.Id, CourseId = course4.Id };

			modelBuilder.Entity<TeacherCourse>().HasData(teacherCourse1, teacherCourse2, teacherCourse3, teacherCourse4);

			#endregion

			#region Таблица связка "Учебная группа - Дисциплина"

			var groupCourse1 = new GroupCourse() { GroupId = group1.Id, CourseId = course1.Id };
			var groupCourse2 = new GroupCourse() { GroupId = group2.Id, CourseId = course1.Id };
			var groupCourse3 = new GroupCourse() { GroupId = group3.Id, CourseId = course1.Id };

			var groupCourse4 = new GroupCourse() { GroupId = group4.Id, CourseId = course2.Id };
			var groupCourse5 = new GroupCourse() { GroupId = group5.Id, CourseId = course2.Id };
			var groupCourse6 = new GroupCourse() { GroupId = group6.Id, CourseId = course2.Id };

			var groupCourse7 = new GroupCourse() { GroupId = group7.Id, CourseId = course3.Id };
			var groupCourse8 = new GroupCourse() { GroupId = group8.Id, CourseId = course3.Id };
			var groupCourse9 = new GroupCourse() { GroupId = group9.Id, CourseId = course3.Id };

			var groupCourse10 = new GroupCourse() { GroupId = group10.Id, CourseId = course4.Id };
			var groupCourse11 = new GroupCourse() { GroupId = group11.Id, CourseId = course4.Id };
			var groupCourse12 = new GroupCourse() { GroupId = group12.Id, CourseId = course4.Id };

			modelBuilder.Entity<GroupCourse>().HasData(groupCourse1, groupCourse2, groupCourse3, groupCourse4, groupCourse5, groupCourse6, groupCourse7, groupCourse8, groupCourse9, groupCourse10, groupCourse11, groupCourse12);

			#endregion

			#region Виды учебных занятий

			var lessonType1 = new LessonType() { Id = 1, Name = "Лекция" };
			var lessonType2 = new LessonType() { Id = 2, Name = "Практическое занятие" };
			var lessonType3 = new LessonType() { Id = 3, Name = "Лабораторное занятие в компьютерном классе" };

			modelBuilder.Entity<LessonType>().HasData(lessonType1, lessonType2, lessonType3);

			#endregion

			#region Учебные единицы

			var teachingUnit1 = new TeachingUnit() { Id = 1, GroupId = group1.Id, TeacherId = teacher1.Id, CourseId = course1.Id, LessonTypeId = lessonType1.Id, CountInPeriodTimeslot = 1 };
			var teachingUnit2 = new TeachingUnit() { Id = 2, GroupId = group2.Id, TeacherId = teacher1.Id, CourseId = course1.Id, LessonTypeId = lessonType2.Id, CountInPeriodTimeslot = 2 };
			var teachingUnit4 = new TeachingUnit() { Id = 4, GroupId = group3.Id, TeacherId = teacher1.Id, CourseId = course1.Id, LessonTypeId = lessonType2.Id, CountInPeriodTimeslot = 2 };
			var teachingUnit3 = new TeachingUnit() { Id = 3, GroupId = group2.Id, TeacherId = teacher1.Id, CourseId = course1.Id, LessonTypeId = lessonType3.Id, CountInPeriodTimeslot = 1 };
			var teachingUnit5 = new TeachingUnit() { Id = 5, GroupId = group3.Id, TeacherId = teacher1.Id, CourseId = course1.Id, LessonTypeId = lessonType3.Id, CountInPeriodTimeslot = 1 };

			var teachingUnit6 = new TeachingUnit() { Id = 6, GroupId = group4.Id, TeacherId = teacher2.Id, CourseId = course2.Id, LessonTypeId = lessonType1.Id, CountInPeriodTimeslot = 1 };
			var teachingUnit7 = new TeachingUnit() { Id = 7, GroupId = group5.Id, TeacherId = teacher2.Id, CourseId = course2.Id, LessonTypeId = lessonType2.Id, CountInPeriodTimeslot = 2 };
			var teachingUnit8 = new TeachingUnit() { Id = 8, GroupId = group5.Id, TeacherId = teacher2.Id, CourseId = course2.Id, LessonTypeId = lessonType3.Id, CountInPeriodTimeslot = 1 };
			var teachingUnit9 = new TeachingUnit() { Id = 9, GroupId = group6.Id, TeacherId = teacher2.Id, CourseId = course2.Id, LessonTypeId = lessonType2.Id, CountInPeriodTimeslot = 2 };
			var teachingUnit10 = new TeachingUnit() { Id = 10, GroupId = group6.Id, TeacherId = teacher2.Id, CourseId = course2.Id, LessonTypeId = lessonType3.Id, CountInPeriodTimeslot = 1 };
			
			var teachingUnit11 = new TeachingUnit() { Id = 11, GroupId = group7.Id, TeacherId = teacher3.Id, CourseId = course3.Id, LessonTypeId = lessonType1.Id, CountInPeriodTimeslot = 1 };
			var teachingUnit12 = new TeachingUnit() { Id = 12, GroupId = group8.Id, TeacherId = teacher3.Id, CourseId = course3.Id, LessonTypeId = lessonType2.Id, CountInPeriodTimeslot = 2 };
			var teachingUnit13 = new TeachingUnit() { Id = 13, GroupId = group8.Id, TeacherId = teacher3.Id, CourseId = course3.Id, LessonTypeId = lessonType3.Id, CountInPeriodTimeslot = 1 };
			var teachingUnit14 = new TeachingUnit() { Id = 14, GroupId = group9.Id, TeacherId = teacher3.Id, CourseId = course3.Id, LessonTypeId = lessonType2.Id, CountInPeriodTimeslot = 2 };
			var teachingUnit15 = new TeachingUnit() { Id = 15, GroupId = group9.Id, TeacherId = teacher3.Id, CourseId = course3.Id, LessonTypeId = lessonType3.Id, CountInPeriodTimeslot = 1 };

			var teachingUnit16 = new TeachingUnit() { Id = 16, GroupId = group10.Id, TeacherId = teacher4.Id, CourseId = course4.Id, LessonTypeId = lessonType1.Id, CountInPeriodTimeslot = 1 };
			var teachingUnit17 = new TeachingUnit() { Id = 17, GroupId = group11.Id, TeacherId = teacher4.Id, CourseId = course4.Id, LessonTypeId = lessonType2.Id, CountInPeriodTimeslot = 2 };
			var teachingUnit18 = new TeachingUnit() { Id = 18, GroupId = group11.Id, TeacherId = teacher4.Id, CourseId = course4.Id, LessonTypeId = lessonType3.Id, CountInPeriodTimeslot = 1 };
			var teachingUnit19 = new TeachingUnit() { Id = 19, GroupId = group12.Id, TeacherId = teacher4.Id, CourseId = course4.Id, LessonTypeId = lessonType2.Id, CountInPeriodTimeslot = 2 };
			var teachingUnit20 = new TeachingUnit() { Id = 20, GroupId = group12.Id, TeacherId = teacher4.Id, CourseId = course4.Id, LessonTypeId = lessonType3.Id, CountInPeriodTimeslot = 1 };

			modelBuilder.Entity<TeachingUnit>().HasData(
				teachingUnit1, teachingUnit2, teachingUnit3, teachingUnit4, teachingUnit5,
				teachingUnit6, teachingUnit7, teachingUnit8, teachingUnit9, teachingUnit10,
				teachingUnit11, teachingUnit12, teachingUnit13, teachingUnit14, teachingUnit15,
				teachingUnit16, teachingUnit17, teachingUnit18, teachingUnit19, teachingUnit20);

			#endregion

			#region Таблица связка "Учебная единица - Тип аудитории по назначению"

			var teachingUnitClassroomTypes = new List<(int teachingUnitId, int classroomTypeId)>()
			{
				(1, 1), (2, 2), (3, 3), (4, 2), (5, 3),
				(6, 1), (7, 2), (8, 3), (9, 2), (10, 3),
				(11, 1), (12, 2), (13, 3), (14, 2), (15, 3),
				(16, 1), (17, 2), (18, 3), (19, 2), (20, 3)
			}.Select(x => new TeachingUnitClassroomType() { TeachingUnitId = x.teachingUnitId, ClassroomTypeId = x.classroomTypeId });

			modelBuilder.Entity<TeachingUnitClassroomType>().HasData(teachingUnitClassroomTypes);

			#endregion

			#region Расписание



			#endregion

			#region Роли

			var role1 = new Role() { Id = 1, Name = "admin", Description = "Администратор веб-приложения" };
			var role2 = new Role() { Id = 2, Name = "methodist", Description = "Методист учебного отдела ВУЗа" };

			modelBuilder.Entity<Role>().HasData(role1, role2);

			#endregion

			#region Страницы

			var webPage1 = new WebPage() { Id = 1, Name = "Редактирование факультетов", Path = @"^(/administration/faculties)" };
			var webPage2 = new WebPage() { Id = 2, Name = "Редактирование кафедр", Path = @"^(/administration/departments)" };

			modelBuilder.Entity<WebPage>().HasData(webPage1, webPage2);

			#endregion

			#region Ограничения "Роль-страница"

			var banRoleWebPage1 = new BanRoleWebPage() { RoleId = role2.Id, WebPageId = webPage2.Id };

			modelBuilder.Entity<BanRoleWebPage>().HasData(banRoleWebPage1);

			#endregion

			#region Пользователи

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

			#endregion
		}
	}
}
