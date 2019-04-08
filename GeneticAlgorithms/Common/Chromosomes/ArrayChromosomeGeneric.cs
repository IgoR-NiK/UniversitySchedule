using System;
using System.Collections.Generic;
using System.Text;

using GeneticAlgorithms.Core;

namespace GeneticAlgorithms.Common.Chromosomes
{
	public class ArrayChromosome<T> : ArrayChromosome
		where T : IEquatable<T>
	{
		public T[] Code { get; private set; }
		public ArrayChromosome(int length) { Code = new T[length]; }

		public void SetCode(Func<int, T> getCodeValue)
		{
			for (int i = 0; i < Code.Length; i++)
				Code[i] = getCodeValue(i);
		}

		public override Array ObjectCode
		{
			get { return Code; }
		}

		public override bool Equals(Chromosome _other)
		{
			if (!(_other is ArrayChromosome<T>)) return false;
			var other = _other as ArrayChromosome<T>;
			if (Code.Length != other.Code.Length) return false;
			for (int i = 0; i < Code.Length; i++)
				if (!Code[i].Equals(other.Code[i])) return false;
			return true;
		}
	}
}
