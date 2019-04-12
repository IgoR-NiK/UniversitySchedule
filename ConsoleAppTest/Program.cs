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

			var sum = 0;
			var sum2 = 0;
			schedules.ForEach(x =>
			{
				var f = Function1.Count(x);
				var f2 = Function3.Count(x);
				sum += f;
				sum2 += f2;
				Console.WriteLine($"Количество неутренних лекций: {f}");
				Console.WriteLine($"Количество избыточных мест: {f2}");
				Console.WriteLine($"Оценка расписания: {EvaluationCalculation.Calculate(x)}");
				Console.WriteLine($"-------------------------------------");
			});

			Console.WriteLine($"Всего ошибок (Лекции): {sum}");
			Console.WriteLine($"Всего ошибок (Избыточные места): {sum2}");

			await scheduleCellRepository.Clear();
			await scheduleCellRepository.AddRangeAsync(schedules.First().ScheduleCells.Select(x => ScheduleCellConverter.Convert(x)));

			/*
						var rnd = new Random(1);
						int Count = 5000;
						int MaxWeight = 5000;
						int[] Weights = Enumerable.Range(0, Count).Select(z => rnd.Next(MaxWeight)).ToArray();
						if (Weights.Sum() % 2 != 0) Weights[0]++;

						var ga = new GeneticAlgorithm<ArrayChromosome<bool>>(() => new ArrayChromosome<bool>(Count), rnd);

						Solutions.AppearenceCount.MinimalPoolSize(ga, 40);
						Solutions.MutationOrigins.Random(ga, 0.5);
						Solutions.CrossFamilies.Random(ga, 0.5);
						Solutions.Selections.Threashold(ga, 40);

						ArrayChromosomeSolutions.Appearences.Bool(ga);
						ArrayChromosomeSolutions.Mutators.Bool(ga);
						ArrayChromosomeSolutions.Crossover.Mix<ArrayChromosome<bool>, bool>(ga);

						ga.Evaluate = chromosome =>
						{
							chromosome.Value =
								1.0 / (1 + Math.Abs(Enumerable.Range(0, Count).Sum(z => Weights[z] * (chromosome.Code[z] ? -1 : 1))));
						};

						for (var i = 0; i < 40; i++)
						{
							ga.MakeIteration();

							Console.Clear();
							Console.WriteLine($"Итерация: {ga.CurrentIteration}");
							Console.WriteLine($"Среднее значение: {ga.Pool.Average(z => z.Value)}");
							Console.WriteLine($"Максимальное значение: {ga.Pool.Max(z => z.Value)}");
							Console.WriteLine($"Средний возраст: {ga.Pool.Average(z => z.Age)}");
						}
						*/

			Console.ReadKey();
		}
	}
}