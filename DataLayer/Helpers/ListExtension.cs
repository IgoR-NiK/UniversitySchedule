using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Helpers
{
	public static class ListExtension
	{
		/// <summary>
		/// Перемешивает элементы списка в случайном порядке по алгоритму Фишера – Йетса.
		/// </summary>
		public static List<T> Shuffle<T>(this List<T> list)
		{
			var result = new List<T>(list);

			var random = new Random();
			for (int i = result.Count - 1; i >= 1; i--)
			{
				int j = random.Next(i + 1);

				T tmp = result[j];
				result[j] = result[i];
				result[i] = tmp;
			}

			return result;
		}
	}
}
