using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;

namespace DataLayer.Algorithms
{
	public interface IAlgorithm
	{
		List<TeachingUnit> GetTeachingUnits(List<TeachingUnit> teachingUnits);

		(Classroom classroom, PeriodTimeslot periodTimeslot) GetTimeslot(TeachingUnit teachingUnit);
	}
}
