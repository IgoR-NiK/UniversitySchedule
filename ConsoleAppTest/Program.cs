using System;
using System.Linq;
using System.Threading.Tasks;

using Repository;
using Repository.MsSqlRepository;

using DataLayer.Converters;
using DataLayer.ScheduleGenerations;


using System.Diagnostics;
using GeneticAlgorithms.Core;
using GeneticAlgorithms.Common.Chromosomes;
using GeneticAlgorithms.Common;

namespace ConsoleAppTest
{
	class Program
	{
		const string ConnectionString = "Data Source=lenovo-pc;Initial Catalog=UniversityScheduleDB;Integrated Security=True";

		static async Task Main(string[] args)
		{
			/*	var connectionOptions = new ConnectionOptions(ConnectionString);

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

				var generation = new RandomScheduleGeneration(10);
				var schedules = generation.Run(classrooms, periodTimeslots, teachingUnits);

				await scheduleCellRepository.Clear();
				await scheduleCellRepository.AddRangeAsync(schedules.First().ScheduleCells.Select(x => ScheduleCellConverter.Convert(x)));
			*/

			var rnd = new Random(1);
			int Count = 5000;
			int MaxWeight = 5000;
			int[] Weights = Enumerable.Range(0, Count).Select(z => rnd.Next(MaxWeight)).ToArray();
			if (Weights.Sum() % 2 != 0) Weights[0]++;

			var ga = new GeneticAlgorithm<ArrayChromosome<bool>>(
				() => new ArrayChromosome<bool>(Count)
				, rnd);

			Solutions.AppearenceCount.MinimalPoolSize(ga, 40);
			Solutions.MutationOrigins.Random(ga, 0.5);
			Solutions.CrossFamilies.Random(ga, z => z * 0.5);
			Solutions.Selections.Threashold(ga, 40);



			ArrayGeneSolutions.Appearences.Bool(ga);
			ArrayGeneSolutions.Mutators.Bool(ga);
			ArrayGeneSolutions.Crossover.Mix(ga);



			ga.Evaluate = chromosome =>
			{
				chromosome.Value =
					1.0 / (1 + Math.Abs(Enumerable.Range(0, Count).Sum(z => Weights[z] * (chromosome.Code[z] ? -1 : 1))));
			};

			var i = 0;
			while (true)
			{				
				var watch = new Stopwatch();
				watch.Start();

				while (watch.ElapsedMilliseconds < 200)
				{
					i++;
					ga.MakeIteration();

					Console.Clear();
					Console.WriteLine($"Итерация: {i}");
					Console.WriteLine($"Среднее значение: {ga.Pool.Average(z => z.Value)}");
					Console.WriteLine($"Максимальное значение: {ga.Pool.Max(z => z.Value)}");
					Console.WriteLine($"Средний возраст: {ga.Pool.Average(z => z.Age)}");
				}

				watch.Stop();
			}

			Console.ReadKey();
		}
	}
}
