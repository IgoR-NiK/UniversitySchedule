using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Вид учебного занятия.
	/// </summary>
	public class LessonType
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public ICollection<TeachingUnit> TeachingUnits { get; set; } = new List<TeachingUnit>();
	}
}
