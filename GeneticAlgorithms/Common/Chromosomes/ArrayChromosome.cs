using System;
using System.Collections.Generic;
using System.Text;

using GeneticAlgorithms.Core;

namespace GeneticAlgorithms.Common.Chromosomes
{
	public abstract class ArrayChromosome : Chromosome
	{
		public abstract Array ObjectCode { get; }

		public void SetObjectCode(Func<int, object> getCodeValue)
		{
			for (int i = 0; i < ObjectCode.Length; i++)
				ObjectCode.SetValue(getCodeValue(i), i);
		}

		public void CheckLength(params ArrayChromosome[] genes)
		{
			for (int i = 0; i < genes.Length; i++)
				if (ObjectCode.Length != genes[i].ObjectCode.Length)
					throw new Exception("Length check failed for ArrayGenes. All genes in algorithm are supposed to have the same length, ot standard solutions will not work");
		}
	}
}
