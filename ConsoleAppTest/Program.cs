﻿using System;
using System.Linq;
using System.Threading.Tasks;

using Repository;
using Repository.MsSqlRepository;

using DataLayer;
using DataLayer.Models;
using DataLayer.Converters;

namespace ConsoleAppTest
{
	class Program
	{
		const string ConnectionString = "Data Source=lenovo-pc;Initial Catalog=UniversityScheduleDB;Integrated Security=True";

		static async Task Main(string[] args)
		{
			var connectionOptions = new ConnectionOptions(ConnectionString);
			var periodTimeslotRepository = new MsSqlPeriodTimeslotRepository(connectionOptions);
			var classroomRepository = new MsSqlClassroomRepository(connectionOptions);

			var periodTimeslots = (await periodTimeslotRepository.GetEntityListAsync())
				.Select(x => PeriodTimeslotConverter.Convert(x)).ToList();

			var classrooms = (await classroomRepository.GetEntityListAsync())
				.Select(x => ClassroomConverter.Convert(x)).ToList();

			var algorithm = new Algorithm();

			



			var result = algorithm.Run();

			Console.ReadKey();
		}
	}
}
