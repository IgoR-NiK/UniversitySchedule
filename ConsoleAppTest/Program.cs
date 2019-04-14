using System;
using System.Linq;
using System.Threading.Tasks;

using Repository;
using Repository.MsSqlRepository;

using DataLayer.Converters;
using DataLayer.ScheduleGenerations;

using GeneticAlgorithms.Core;
using GeneticAlgorithms.Common.Chromosomes;
using GeneticAlgorithms.Common.Solutions;
using DataLayer.ScheduleGenerations.Genetic;
using DataLayer.ScheduleGenerations.Genetic.EvaluationFunctions;

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
			var teachingUnitRepository = new MsSqlTeachingUnitRepository(connectionOptions);
			var scheduleCellRepository = new MsSqlScheduleCellRepository(connectionOptions);

			var periodTimeslots = (await periodTimeslotRepository.GetEntityListAsync())
				.Select(x => PeriodTimeslotConverter.Convert(x)).ToList();

			var classrooms = (await classroomRepository.GetEntityListAsync())
				.Select(x => ClassroomConverter.Convert(x)).ToList();

			var teachingUnits = (await teachingUnitRepository.GetEntityListAsync())
				.Select(x => TeachingUnitConverter.Convert(x)).ToList();

			var generation = new GeneticScheduleGeneration(10);
			var schedules = generation.Run(classrooms, periodTimeslots, teachingUnits);

			var i = 1;
			var sum = 0;
			var sum2 = 0;
			var sum3 = 0;
			var sum4 = 0;
			var sum5 = 0;
			var sum6 = 0;
			schedules.ForEach(x =>
			{
				var f = Function1.Count(x);
				var f2 = Function3.Count(x);
				var f3 = Function4.Count(x);
				var f4 = Function5.Count(x);
				var f5 = Function6.Count(x);
				var f6 = Function7.Count(x);
				sum += f;
				sum2 += f2;
				sum3 += f3;
				sum4 += f4;
				sum5 += f5;
				sum6 += f6;
				Console.WriteLine($"Расписание {i++}");
				Console.WriteLine($"Количество неутренних лекций: {f}");
				Console.WriteLine($"Количество избыточных мест: {f2}");
				Console.WriteLine($"Количество превышений пар в день для преподавателей: {f3}");
				Console.WriteLine($"Количество окон для преподавателей: {f4}");
				Console.WriteLine($"Количество превышений пар в день для студентов: {f5}");
				Console.WriteLine($"Количество окон для студентов: {f6}");
				Console.WriteLine($"Оценка расписания: {EvaluationCalculation.Calculate(x)}");
				Console.WriteLine($"-------------------------------------");
			});

			Console.WriteLine($"Всего ошибок (Лекции): {sum}");
			Console.WriteLine($"Всего ошибок (Избыточные места): {sum2}");
			Console.WriteLine($"Всего ошибок (Количество превышений пар в день для преподавателей): {sum3}");
			Console.WriteLine($"Всего ошибок (Количество окон для преподавателей): {sum4}");
			Console.WriteLine($"Всего ошибок (Количество превышений пар в день для студентов): {sum5}");
			Console.WriteLine($"Всего ошибок (Количество окон для студентов): {sum6}");

			await scheduleCellRepository.Clear();
			await scheduleCellRepository.AddRangeAsync(schedules.First().ScheduleCells.Select(x => ScheduleCellConverter.Convert(x)));
			
			Console.ReadKey();
		}
	}
}