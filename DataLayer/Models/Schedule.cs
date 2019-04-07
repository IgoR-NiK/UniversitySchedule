using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
	public class Schedule
	{
		public int ClassroomId { get; set; }
		public Classroom Classroom { get; set; }

		public int PeriodTimeslotId { get; set; }
		public PeriodTimeslot PeriodTimeslot { get; set; }

		public int TeachingUnitId { get; set; }
		public TeachingUnit TeachingUnit { get; set; }

		public Schedule() { }

		public Schedule(Classroom classroom, PeriodTimeslot periodTimeslot, TeachingUnit teachingUnit)
		{
			ClassroomId = classroom.Id;
			Classroom = classroom;

			PeriodTimeslotId = periodTimeslot.Id;
			PeriodTimeslot = periodTimeslot;

			TeachingUnitId = teachingUnit.Id;
			TeachingUnit = teachingUnit;
		}

		public override string ToString()
		{
			return $"{Classroom} - {PeriodTimeslot} - {TeachingUnit}";
		}
	}
}
