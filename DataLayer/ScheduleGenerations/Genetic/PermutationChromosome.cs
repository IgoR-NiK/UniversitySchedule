using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Models;
using GeneticAlgorithms.Core;

namespace DataLayer.ScheduleGenerations.Genetic
{
	public class PermutationChromosome : Chromosome, IEquatable<PermutationChromosome>
	{
		public Schedule Schedule { get; set; }
		public int[] Code { get; set; }

		public PermutationChromosome(int length)
		{
			Code = Enumerable.Range(0, length).ToArray();
		}

		/// <summary>
		/// Перемешивает элементы списка в случайном порядке по алгоритму Фишера – Йетса.
		/// </summary>
		public void Shuffle(Random random)
		{
			for (int i = Code.Length - 1; i >= 1; i--)
			{
				var j = random.Next(i + 1);

				var tmp = Code[j];
				Code[j] = Code[i];
				Code[i] = tmp;
			}
		}

		/// <summary>
		/// Меняет местами рядом стоящие элементы массива
		/// </summary>
		public void Swap(Random random)
		{
			var i = random.Next(Code.Length - 1);

			var tmp = Code[i];
			Code[i] = Code[i + 1];
			Code[i + 1] = tmp;
		}

		public bool Equals(PermutationChromosome other)
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
