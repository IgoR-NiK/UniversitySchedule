using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Helpers;
using DataLayer.Models;

namespace DataLayer.Algorithms
{
	public class RandomAlgorithm : IAlgorithm
	{
		Random Random { get; } = new Random();

		public List<TeachingUnit> GetTeachingUnits(List<TeachingUnit> teachingUnits)
		{
			return teachingUnits.Shuffle();
		}

		public (Classroom classroom, PeriodTimeslot periodTimeslot) GetTimeslot(TeachingUnit teachingUnit)
		{
			var index = Random.Next(teachingUnit.FreeTimeslots.Count);
			var timeslot = teachingUnit.FreeTimeslots[index];

			return timeslot;
		}
	}
}
