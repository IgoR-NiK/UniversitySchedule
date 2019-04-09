using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithms.Core
{
	public abstract class Chromosome
	{
		public double Value { get; set; }
		public int Age { get; internal set; }

		public bool Evaluated { get; internal set; }
		public int Generation { get; internal set; }

		public override string ToString()
		{
			return $"Value = {Value}, Age = {Age}";
		}
	}
}
