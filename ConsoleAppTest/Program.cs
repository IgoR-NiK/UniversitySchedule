using System;

using DataLayer;

namespace ConsoleAppTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var algorithm = new Algorithm();

			Console.WriteLine("Hello World!");
			Console.WriteLine(algorithm.Run());

			Console.ReadKey();
		}
	}
}
