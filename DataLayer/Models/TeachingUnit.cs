﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
	public class TeachingUnit
	{
		public int Id { get; set; }

		public int CountInPeriodTimeslot { get; set; }

		public int GroupId { get; set; }
		public Group Group { get; set; }

		public int TeacherId { get; set; }
		public Teacher Teacher { get; set; }

		public int CourseId { get; set; }
		public Course Course { get; set; }

		public int LessonTypeId { get; set; }
		public LessonType LessonType { get; set; }

		public List<ClassroomType> ClassroomTypes { get; set; } = new List<ClassroomType>();

		public List<(int classroomId, int periodTimeslotId)> FreeTimeslots { get; set; } = new List<(int, int)>();
		

		public override bool Equals(object obj)
		{
			return obj is TeachingUnit teachingUnit ? Id == teachingUnit.Id : false;
		}
	}
}
