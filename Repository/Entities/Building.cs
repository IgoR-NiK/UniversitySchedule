using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Учебный корпус.
	/// </summary>
	public class Building
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[StringLength(30)]
		public string ShortName { get; set; }

		public ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();
	}
}
