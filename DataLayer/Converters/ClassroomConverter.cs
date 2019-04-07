using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Models;
using DbClassroom = Repository.Entities.Classroom;

namespace DataLayer.Converters
{
	public static class ClassroomConverter
	{
		public static Classroom Convert(DbClassroom dbClassroom)
		{
			if (dbClassroom == null) return null;

			return new Classroom()
			{
				Id = dbClassroom.Id,
				Name = dbClassroom.Name,
				Number = dbClassroom.Number,
				Capacity = dbClassroom.Capacity,

				ClassroomTypeId = dbClassroom.ClassroomTypeId,
				ClassroomType = ClassroomTypeConverter.Convert(dbClassroom.ClassroomType),

				BuildingId = dbClassroom.BuildingId,
				Building = BuildingConverter.Convert(dbClassroom.Building),

				BanPeriodTimeslots = dbClassroom.BanClassroomPeriodTimeslots
										.Select(x => PeriodTimeslotConverter.Convert(x.PeriodTimeslot))
										.ToList()
			};			
		}

		public static DbClassroom Convert(Classroom classroom)
		{
			if (classroom == null) return null;

			return new DbClassroom()
			{
				Id = classroom.Id,
				Name = classroom.Name,
				Number = classroom.Number,
				Capacity = classroom.Capacity,
				ClassroomTypeId = classroom.ClassroomTypeId,
				BuildingId = classroom.BuildingId				
			};
		}
	}
}
