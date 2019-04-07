using System;
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

			var algorithm = new Algorithm();

			var periodTimeslots = (await periodTimeslotRepository.GetEntityListAsync()).Select(x => PeriodTimeslotConverter.Convert(x)).ToList();

			periodTimeslots.ForEach(x =>
			{
				Console.WriteLine($"Id: {x.Id}");
				Console.WriteLine($"Week: {x.Week.Name}");
				Console.WriteLine($"Day: {x.Day.Name}");
				Console.WriteLine($"DayTimeslot: {x.DayTimeslot.Name} - {x.DayTimeslot.StartTime.ToShortTimeString()}");
				Console.WriteLine("---------------------------------------");
			});

			var result = algorithm.Run();

			Console.ReadKey();
		}
	}
}
