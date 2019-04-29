using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;

namespace DataLayer.ScheduleGenerations.Genetic.EvaluationFunctions
{
	public static class EvaluationCalculation
	{
		static double[] Weights { get; } = new double[] { 8, 1, 1000, 3, 10, 5, 15 };

		public static double Calculate(Schedule schedule)
		{
			var value = 0.0;

			value += Weights[0] * Function1.Value(schedule);
	//		value += Weights[1] * Function2.Value(schedule);
			value += Weights[2] * Function3.Value(schedule);
			value += Weights[3] * Function4.Value(schedule);
			value += Weights[4] * Function5.Value(schedule);
			value += Weights[5] * Function6.Value(schedule);
			value += Weights[6] * Function7.Value(schedule);

			return value;				
		}
	}
}
