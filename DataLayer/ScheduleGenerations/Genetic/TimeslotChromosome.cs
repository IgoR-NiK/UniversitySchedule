using GeneticAlgorithms.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ScheduleGenerations.Genetic
{
	public class TimeslotChromosome : Chromosome, IEquatable<TimeslotChromosome>
	{
		public int Code { get; set; }

		public bool Equals(TimeslotChromosome other)
		{
			return Code == other.Code;
		}
	}
}
