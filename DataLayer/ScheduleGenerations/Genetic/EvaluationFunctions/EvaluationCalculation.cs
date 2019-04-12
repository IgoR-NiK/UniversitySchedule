﻿using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;

namespace DataLayer.ScheduleGenerations.Genetic.EvaluationFunctions
{
	public static class EvaluationCalculation
	{
		static double[] Weights { get; } = new double[] { 1, 1, 100 };

		public static double Calculate(Schedule schedule)
		{
			var value = 0.0;

			value += Weights[0] * Function1.Value(schedule);
			value += Weights[1] * Function2.Value(schedule);
			value += Weights[2] * Function3.Value(schedule);

			return value;				
		}
	}
}
