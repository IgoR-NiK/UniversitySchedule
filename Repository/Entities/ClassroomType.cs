using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Тип аудитории по назначению.
	/// </summary>
	public class ClassroomType
	{
		public int Id { get; set; }

		[Required]
		[StringLength(300)]
		public string Name { get; set; }

		public ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();
		public ICollection<TeachingUnitClassroomType> TeachingUnitClassroomTypes { get; set; } = new List<TeachingUnitClassroomType>();
	}
}
