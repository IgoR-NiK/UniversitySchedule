using System;
using System.Collections.Generic;
using System.Text;

using GeneticAlgorithms.Core;

namespace GeneticAlgorithms.Common.Chromosomes
{
	public class ArrayChromosome<T> : Chromosome, IEquatable<ArrayChromosome<T>>
		where T : IEquatable<T>
	{
		public T[] Code { get; }


		public ArrayChromosome(int length)
		{
			Code = new T[length];
		}


		public void SetCode(Func<int, T> getCodeValue)
		{
			for (int i = 0; i < Code.Length; i++)
				Code[i] = getCodeValue(i);
		}

		public void CheckLength(params ArrayChromosome<T>[] genes)
		{
			for (int i = 0; i < genes.Length; i++)
				if (Code.Length != genes[i].Code.Length)
					throw new Exception("Все хромосомы должны быть одинаковой длинны");
		}

		public bool Equals(ArrayChromosome<T> other)
		{
			if (Code.Length != other.Code.Length)			
				return false;
			
			for (var i = 0; i < Code.Length; i++)			
				if (!Code[i].Equals(other.Code[i]))				
					return false;			

			return true;
		}
	}
}
